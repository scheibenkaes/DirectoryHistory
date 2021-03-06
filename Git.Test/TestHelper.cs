//  
//  TestHelper.cs
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
using System.Diagnostics;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{
	public class TestHelper
	{
		private static readonly IHistoryProvider git = new HistoryProvider();
		
		public static IDirectoryWithHistory CreateTestRepo ()
		{
			RemoveTestRepo ();
			CreateDir ();
			return CreateRepo ();
		}

		private static IDirectoryWithHistory CreateRepo ()
		{
			return git.CreateRepository (TestData.TEMP_DIR);
		}

		
		private static void CreateDir ()
		{
			if (!Directory.Exists (TestData.TEMP_DIR)) {
				Directory.CreateDirectory (TestData.TEMP_DIR);
			}
		}
		
		public static void RemoveTestRepo ()
		{
			"rm".RunCommand (string.Format (" -Rf {0}", TestData.TEMP_DIR));
		}
		
		public static void CreateDirectory (string path)
		{
			Directory.CreateDirectory (path);
		}
		
		public static string CreateTestFilePath (string file)
		{
			var osindependentFilename = file.Replace ("/", Path.DirectorySeparatorChar.ToString ());
			return Path.Combine (TestData.TEMP_DIR, osindependentFilename);
		}
		
	}
}
