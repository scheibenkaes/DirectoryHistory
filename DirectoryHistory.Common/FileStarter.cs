//  
//  FileStarter.cs
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
using System.Diagnostics;
using System.IO;

namespace DirectoryHistory.Common
{
	public static class FileStarter
	{
		/// <summary>
		/// Starts a file given in path.
		/// </summary>
		/// <param name="path">
		/// Must not be empty or null and must exist.
		/// </param>
		public static void StartFile (string path)
		{
			if (string.IsNullOrEmpty (path)) {
				throw new ArgumentNullException ("path");
			}
			if (!File.Exists (path)) {
				throw new FileNotFoundException (path);
			}
			
			Process.Start (path);
		}
	}
}

