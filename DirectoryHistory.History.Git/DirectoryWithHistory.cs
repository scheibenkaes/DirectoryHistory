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

namespace DirectoryHistory.History.Git
{
	public class DirectoryWithHistory : IDirectoryWithHistory
	{
		public bool IsClean { get; private set; }


		public bool IsRootDirectory { get; set; }


		public string Path { get; private set; }


		public IEnumerable<IDirectoryWithHistory> ChildDirectories { get; private set; }

		public DirectoryWithHistory (string path)
		{
			Path = path;
			
			ReadSubDirectories ();
		}

		private void ReadSubDirectories ()
		{
			var directories = Directory.GetDirectories (Path);
			var subDirToBeAdded = new List<IDirectoryWithHistory> ();
			foreach (var dir in directories) {
				var dirWithHistory = new DirectoryWithHistory (dir) { 
					IsRootDirectory = false 
				};
				subDirToBeAdded.Add (dirWithHistory);
			}
			
			ChildDirectories = subDirToBeAdded;
		}
		
	}
}
