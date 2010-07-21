//  
//  Extensions.cs
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
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using GitSharp;

namespace DirectoryHistory.History.Git
{


	public static class Extensions
	{
		public static IDirectoryWithHistory RepositoryToDirectoryWithHistory (this Repository repo, IHistoryProvider provider, string path)
		{
			var dir = new DirectoryWithHistory (provider, path) { IsRootDirectory = true };
			
			return dir;
		}
		
		public static string ReducePath (this string path, string reducedWith)
		{
			if (string.IsNullOrEmpty (path)) {
				throw new ArgumentException ("path is null or empty");
			}
			var full = new Regex (path.Replace (@"\", @"\\"));
			
			var stripped = full.Replace (reducedWith, "");
			
			if (stripped.StartsWith (Path.DirectorySeparatorChar.ToString ())) {
				return stripped.Remove (0, 1);
			}
			return stripped;
		}
		
	}
}
