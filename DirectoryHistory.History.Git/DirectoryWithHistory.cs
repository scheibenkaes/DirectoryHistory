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

		private Repository repository;

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
			
			// TODO Make this mor reasonable
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
				if (IsRootDirectory) {
					return FileStatus.Committed; 
				}
				// Git repositories only can be not under version controll or committed
				// NOTE Git doesn't realize folders if they are empty
				if (DirectoryIsEmpty ()) {
					return FileStatus.NotUnderVersionControl;
				}
				if (repository.Index.Status.Untracked.Contains (PathInRepository) || AUntrackedFileContainsThisDirectory ()) {
					return FileStatus.NotUnderVersionControl;
				}
				return FileStatus.Committed;
			}
		}

		private bool AUntrackedFileContainsThisDirectory ()
		{
			return repository.Status.Untracked.Any (file => file.StartsWith (PathInRepository));
		}


		private bool DirectoryIsEmpty ()
		{
			return !Directory.GetFiles (Path).Any ();
		}
		
		public bool IsBinaryFile ()
		{
			return false;
		}
		
		public string GetContentForVersion (IFileVersion version)
		{
			throw new InvalidOperationException(Catalog.GetString ("This operation (GetContentForVersion) is not valid for a directory."));
		}
	}
}
