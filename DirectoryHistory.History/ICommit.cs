//  
//  ICommit.cs
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
	/// Describes the datastructure which is used to create a new version of a file
	/// </summary>
	public interface ICommit
	{
		/// <summary>
		/// File to be committed
		/// </summary>
		IFileWithHistory File { get; }

		/// <summary>
		/// A user entered message which is used to add additional information
		/// </summary>
		string Comment { get; }
	}
}

