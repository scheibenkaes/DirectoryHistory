//  
//  FileStatus.cs
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


	public enum FileStatus
	{
		/// <summary>
		/// File is not yet under Version Control
		/// </summary>
		NotUnderVersionControl,
		
		/// <summary>
		/// File is under version control and has uncommited changes
		/// </summary>
		Changed,
		
		/// <summary>
		/// All changes have been committed
		/// </summary>
		Committed,
		
		/// <summary>
		/// The file/folder has an unknown status. Which is mostly bad.
		/// </summary>
		Unknown
	}
}
