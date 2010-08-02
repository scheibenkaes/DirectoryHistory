//  
//  DirectoryWithHistory.cs
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
using System.IO;
using System.Linq;

using Mono.Unix;

using GitSharp;

namespace DirectoryHistory.History.Git
{
	public class DirectoryWithHistory : IDirectoryWithHistory
	{
		private readonly string[] IgnoredFolders = new string[] { ".git" };

		public bool IsClean { get; private set; }

		public bool IsRootDirectory { get; set; }

		public string Path { get; private set; }

		public IEnumerable<IDirectoryWithHistory> ChildDirectories { get; private set; }

		public IEnumerable<IFileWithHistory> ChildFiles { get; private set; }

		public IHistoryProvider Provider { get; private set; }

		private readonly Repository repository;

		public DirectoryWithHistory (IHistoryProvider provider, string path)
		{
			if (provider == null) {
				throw new ArgumentNullException ("provider");
			}
			Path = path;
			
			Provider = provider;
			
			repository = ((HistoryProvider)provider).Repository;
			
			ReadSubDirectories ();
			
			ReadContainingFiles ();
		}

		private void ReadContainingFiles ()
		{
			var files = Directory.GetFiles (Path).ToList ();
			ChildFiles = files.ConvertAll<IFileWithHistory> (f => new FileWithHistory (Provider, f));
		}


		private void ReadSubDirectories ()
		{
			var directories = Directory.GetDirectories (Path).ToList ();
			
			// TODO Make this more reasonable
			var ignoredDirectories = directories.Where (dir => dir.EndsWith (IgnoredFolders[0]));
			
			var subDirToBeAdded = new List<IDirectoryWithHistory> ();
			foreach (var dir in directories.Except (ignoredDirectories)) {
				var dirWithHistory = new DirectoryWithHistory (Provider, dir) { IsRootDirectory = false };
				subDirToBeAdded.Add (dirWithHistory);
			}
			
			ChildDirectories = subDirToBeAdded;
		}

		public IEnumerable<IFileVersion> History {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public string PathInRepository {
			get {
				var prov = (HistoryProvider)Provider;
				return Extensions.ReducePath (prov.Repository.WorkingDirectory, Path);
			}
		}

		public FileStatus Status {
			get {
				// Git repositories only can be not under version controll or committed
				// NOTE Git doesn't realize folders if they are empty
				if (DirectoryIsEmpty ()) {
					return FileStatus.NotUnderVersionControl;
				}
				if (repository.Index.Status.Untracked.Contains (PathInRepository) || AUntrackedFileIsContainedInThisDirectory ()) {
					return FileStatus.NotUnderVersionControl;
				}
				if (ContainsAFileWithState (FileStatus.Changed)) {
					return FileStatus.Changed;
				}
				
				return FileStatus.Committed;
			}
		}

		private bool ContainsAFileWithState (FileStatus status)
		{
			var allChildFiles = GetAllContainedFiles (this);
			return allChildFiles.Any (f => f.Status == status);
		}

		private static IEnumerable<IFileWithHistory> GetAllContainedFiles (IDirectoryWithHistory dir)
		{
			foreach (var subdir in dir.ChildDirectories) {
				foreach (var item in GetAllContainedFiles (subdir)) {
					yield return item;
				}
			}
			foreach (var item in dir.ChildFiles) {
				yield return item;
			}
		}


		private bool AUntrackedFileIsContainedInThisDirectory ()
		{
			return repository.Status.Untracked.Any (file => file.StartsWith (PathInRepository));
		}


		private bool DirectoryIsEmpty ()
		{
			return !Directory.GetFileSystemEntries (Path).Any ();
		}

		public bool IsBinaryFile ()
		{
			return false;
		}

		public byte[] GetBinaryContentForVersion (IFileVersion version)
		{
			throw new System.NotImplementedException ();
		}
	}
}
