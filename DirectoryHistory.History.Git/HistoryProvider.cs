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
		
		public Author Author {
			private set;
			get;
		}
		
		public HistoryProvider ()
		{
			Author = new Author(Environment.UserName, "TODO@FIXME.COM");
		}

		public IDirectoryWithHistory LoadDirectory (string path)
		{
			if (string.IsNullOrEmpty (path) || !Directory.Exists (path)) {
				throw new HistoryException ("Path must exist and must not be empty!");
			}
			
			if (Repository == null || Repository.WorkingDirectory != path) {
				Repository = new Repository (path);
			}			
			
			return new DirectoryWithHistory (this, path) { IsRootDirectory = true };
		}

		public bool IsARepository (string path)
		{
			return Repository.IsValid (path);
		}

		public IDirectoryWithHistory CreateRepository (string path)
		{
			if (IsARepository (path) && Repository == null) {
				throw new HistoryException ("There's already a repository at the given path " + path);
			}
			Repository = Repository.Init (path);
			
			return Repository.RepositoryToDirectoryWithHistory (this, path);
		}

		public void AddFile (IFileWithHistory file)
		{
			Repository.Index.Add (file.PathInRepository);
		}
		
		public void CommitChanges (ICommit commit)
		{
//			if (!Repository.Status.Modified.Contains (commit.File.Path)) {
//				throw new InvalidOperationException ("Can't commit a not staged file");
//			}
			
			AddFile (commit.File);
				
			Repository.Commit (commit.Comment, Author);
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
			if (!File.Exists (path)) {
				throw new FileNotFoundException (string.Format (Catalog.GetString ("The file {0} isn't existing!"), path));
			}
			return new FileWithHistory (this, path);
		}
		
		public IDirectoryWithHistory GetDirectory (string path)
		{
			if (!Directory.Exists (path)) {
				throw new DirectoryNotFoundException (string.Format (Catalog.GetString ("The directory under {0} is not existing"), path));
			}
			return new DirectoryWithHistory (this, path);
		}
		
		public IFileWithHistory GetFileOrDirectory (string path)
		{
			if (Directory.Exists (path)) {
				return GetDirectory (path);
			}
			else if (File.Exists (path)) {
				return GetFile (path);
			}
			throw new FileNotFoundException (Catalog.GetString ("The file or directory you tried to open does not exist! ({0})"), path);
		}
		
		public event EventHandler<DirectoryStatusWasUpdatedEventArgs> DirectoryWasUpdated;
		
	}
}
