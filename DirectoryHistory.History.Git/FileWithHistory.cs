//  
//  FileWithHistory.cs
//  
//  Author:
//       Benjamin Klüglein <scheibenkaes@googlemail.com>
// 
//  Copyright (c) 2010 Benjamin Klüglein
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

using Mono.Unix;

using GitSharp;

using GitCommit = GitSharp.Commit;

using DirectoryHistory.Common;

namespace DirectoryHistory.History.Git
{
	public class FileWithHistory : IFileWithHistory
	{
		public string Path { get; private set; }

		public IHistoryProvider Provider { get; private set; }

		private Repository repository;

		public FileStatus Status {
			get {
				var status = repository.Index.Status;
				var pathToFile = PathInRepository;
				if (status.Untracked.Contains (pathToFile)) {
					return FileStatus.NotUnderVersionControl;
				} else if (status.Modified.Contains (pathToFile) || status.Added.Contains (pathToFile)) {
					return FileStatus.Changed;
				}
				
				var isClean = AmIClean (status);
				
				if (isClean) {
					return FileStatus.Committed;
				}
				
				return FileStatus.Unknown;
			}
		}

		private bool AmIClean (RepositoryStatus status)
		{
			var filesWithChanges = new List<string> ();
			
			filesWithChanges.AddRange (status.Added);
			filesWithChanges.AddRange (status.Modified);
			filesWithChanges.AddRange (status.Staged);
			
			return !filesWithChanges.Contains (Path);
		}


		public FileWithHistory (IHistoryProvider provider, string path)
		{
			Provider = provider;
			Path = path;
			
			repository = ((HistoryProvider)provider).Repository;
		}

		private bool CommitIncludedThisFile (GitSharp.Commit commit)
		{
			return commit.Changes.Any (ChangeEffectedThisFile);
		}

		private bool ChangeEffectedThisFile (GitSharp.Change change)
		{
			return change.Path == PathInRepository;
		}

		private IEnumerable<GitSharp.Commit> CommitsWithThisFile (GitSharp.Commit commit, IEnumerable<GitSharp.Commit> commits)
		{
			var hasCommitThisFile = CommitIncludedThisFile (commit);
			var newList = commits.ToList ();
			if (hasCommitThisFile) {
				newList.Add (commit);
			}
			
			if (commit.HasParents) {
				return CommitsWithThisFile (commit.Parent, newList);
			} else {
				return newList;
			}
		}

		public IEnumerable<IFileVersion> History {
			get {
				var branch = repository.CurrentBranch;
				var lastCommit = branch.CurrentCommit;
				
				var commitsWithThisFile = CommitsWithThisFile (lastCommit, Enumerable.Empty<GitSharp.Commit> ());
				
				return TransformCommitsIntoFileVersions (commitsWithThisFile).ToList ();
			}
		}

		private IEnumerable<IFileVersion> TransformCommitsIntoFileVersions (IEnumerable<GitSharp.Commit> commitsWithThisFile)
		{
			foreach (GitSharp.Commit commit in commitsWithThisFile) {
				yield return new FileVersion { ID = commit.Hash, CreationAt = commit.AuthorDate.DateTime, Commit = new MyCommit { File = this, Comment = commit.Message } };
			}
			yield break;
		}

		public string GetContentForVersion (IFileVersion version)
		{
			
			return FindLeafForVersion (version).Data;
		}
		
		public byte[] GetBinaryContentForVersion (IFileVersion version)
		{
			return FindLeafForVersion (version).RawData;
		}
		
		private GitSharp.Leaf FindLeafForVersion (IFileVersion version)
		{
			var commit = repository.Get<GitSharp.Commit> (version.ID);
			if (commit == null) {
				throw new HistoryException (Catalog.GetString (string.Format ("File version {0} is not contained in this commit!", version.ID)));
			}
			
			return GetMeFromTree (commit.Tree).First (CollectionHelper.NotNull);
		}
		
		private IEnumerable<Leaf> GetMeFromTree (Tree tree)
		{
			var leaf = GetMeFromLeaves (tree.Leaves);
			if (leaf == null) {
				foreach (var subTree in tree.Trees) {
					yield return GetMeFromLeaves (subTree.Leaves);
				}
			}
			yield return leaf;
		}		
		
		
		private Leaf GetMeFromLeaves (IEnumerable<Leaf> leaves)
		{
			return leaves.Where (l => l.Path == PathInRepository).FirstOrDefault ();
		}
		
		public string PathInRepository {
			get { return repository.WorkingDirectory.ReducePath ( Path); }
		}
	}
}
