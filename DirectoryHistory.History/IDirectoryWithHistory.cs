//  
//  IDirectoryWithHistory.cs
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

namespace DirectoryHistory.History
{
	public interface IDirectoryWithHistory
	{
		/// <summary>
		/// string representation of the folder on disk
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Used to detect if a directory has no uncommited changes
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		bool IsClean ();

		/// <summary>
		/// Detect if this directory was the the originally loaded root directory.
		/// Is used to prevent the user from going further up the directory tree then the originally loaded folder.
		/// </summary>
		/// <returns>
		/// true if the directory from IDirectoryWithHistory.Path was the originally loaded directory
		/// </returns>
		bool IsRootDirectory ();

		/// <summary>
		/// Get all directories in this directory
		/// </summary>
		IEnumerable<IDirectoryWithHistory> ChildDirectories { get; }
		
		
	}
}
