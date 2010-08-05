//  
//  IHistoryProvider.cs
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

namespace DirectoryHistory.History
{
	/// <summary>
	/// Describes the interface to the used Version Control System. E.g. Git, Bazaar etc.
	/// </summary>
	public interface IHistoryProvider: IDisposable
	{		
		/// <summary>
		/// Load the directory form the path given
		/// </summary>
		/// <param name="path">
		/// A <see cref="System.String"/> pointing to the path
		/// </param>
		/// <returns>
		/// A <see cref="IDirectoryWithHistory"/>
		/// </returns>
		IDirectoryWithHistory LoadDirectory (string path);
		
		/// <summary>
		/// Used to determine if a path is a repository and can therefore be loaded into the application.
		/// </summary>
		/// <param name="path">
		/// Path on disk
		/// </param>
		/// <returns>
		/// True if path is a directory and is a Git, Bazaar or whatever repository
		/// </returns>
		bool IsARepository (string path);
		
		/// <summary>
		/// Create a repository in the given directory.
		/// </summary>
		/// <param name="path">
		/// Path on file system
		/// </param>
		/// <returns>
		/// The newly created repository as a <see cref="IDirectoryWithHistory"/>
		/// </returns>
		IDirectoryWithHistory CreateRepository (string path);
		
		/// <summary>
		/// Add the given file to the repository
		/// </summary>
		/// <param name="file">
		/// A <see cref="IFileWithHistory"/>
		/// </param>
		void AddFile (IFileWithHistory file);
		
		/// <summary>
		/// Convenience method <see cref="AddFile"/>
		/// </summary>
		/// <param name="param">
		/// The files to be added
		/// </param>
		void AddFile (params string[] files);
		
		/// <summary>
		/// Retrieve the file corresponding to the given path.
		/// </summary>
		/// <param name="path">
		/// The path to the file, absolute or 'in-repo'
		/// </param>
		/// <returns>
		/// A <see cref="IFileWithHistory"/>
		/// </returns>
		IFileWithHistory GetFile (string path);
		
		/// <summary>
		/// Archive changes to the repository
		/// </summary>
		/// <param name="commit">
		/// A <see cref="ICommit"/>
		/// </param>
		void CommitChanges (ICommit commit);
		
		/// <summary>
		/// Provides access to a directory in the repo
		/// </summary>
		/// <param name="path">
		/// Path on the disk!
		/// </param>
		/// <returns>
		/// A <see cref="IDirectoryWithHistory"/>
		/// </returns>
		IDirectoryWithHistory GetDirectory (string path);
		
		/// <summary>
		/// Provides access to the repository nonetheless if path is a file or a directory
		/// </summary>
		/// <param name="path">
		/// Path of the file/directory
		/// </param>
		/// <returns>
		/// A instance of <see cref="IFileWithHistory"/> or <see cref="IDirectoryWithHistory"/>
		/// </returns>
		IFileWithHistory GetFileOrDirectory (string path);
	}
}
