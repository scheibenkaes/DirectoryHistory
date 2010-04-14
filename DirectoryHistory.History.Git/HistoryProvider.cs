//  
//  HistoryProvider.cs
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
using System.IO;

using Mono.Unix;

using GitSharp;

namespace DirectoryHistory.History.Git
{
	public class HistoryProvider : IHistoryProvider, IDisposable
	{
		public Repository Repository {get; private set;}

		public IDirectoryWithHistory LoadDirectory (string path)
		{
			if (string.IsNullOrEmpty (path) || !Directory.Exists (path)) {
				throw new Exception ("Path must exist and must not be empty!");
			}
			
			Repository = new Repository (path);
			
			return new DirectoryWithHistory (this, path) { IsRootDirectory = true };
		}

		public bool IsARepository (string path)
		{
			return Repository.IsValid (path);
		}

		public IDirectoryWithHistory CreateRepository (string path)
		{
			if (IsARepository (path) && Repository == null) {
				throw new Exception ("There's already a repository at the given path " + path);
			}
			Repository = Repository.Init (path);
			
			return Repository.RepositoryToDirectoryWithHistory (this, path);
		}

		public void AddFile (IFileWithHistory file)
		{
			var index = Repository.Index;
			index.Add (file.PathInRepository);
		}
		
		public void Dispose ()
		{
			if (Repository != null) {
				Repository.Dispose ();
				Repository = null;
			}
		}
		
		public IFileWithHistory GetFile (string path)
		{
			return new FileWithHistory (this, path);
		}
		
		public event EventHandler<DirectoryStatusWasUpdatedEventArgs> DirectoryWasUpdated;
		
	}
}
