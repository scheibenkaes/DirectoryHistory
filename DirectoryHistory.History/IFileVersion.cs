//  
//  IFileVersion.cs
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
	/// Common interface for a version of a single file.
	/// </summary>
	public interface IFileVersion
	{
		/// <summary>
		/// Unique identifier for this version
		/// </summary>
		string ID { get; }

		/// <summary>
		/// Time this version of the file was created
		/// </summary>
		DateTime CreationAt { get; }
		
		/// <summary>
		/// The file this version belongs to
		/// </summary>
		IFileWithHistory File { get; }
	}
}

