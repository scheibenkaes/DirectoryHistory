//  
//  FileVersion.cs
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

using DirectoryHistory.History;

namespace DirectoryHistory.History.Git
{
	public class FileVersion : IFileVersion
	{
		public string ID { get; set; }


		public DateTime CreationAt { get; set; }


		public ICommit Commit { get; set; }

		
		public override string ToString ()
		{
			return string.Format("[FileVersion: ID={0}, CreationAt={1}, File={2}]", ID, CreationAt, Commit.File.PathInRepository);
		}
	}
}

