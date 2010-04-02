//  
//  TestData.cs
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

using GitSharp;

namespace Git.Test
{


	public static class TestData
	{
		public const string TEMP_DIR = "/tmp/gittest";
		
		public static readonly string DIR_WO_GIT = Path.Combine (TEMP_DIR, "wo");
		
		public static readonly string DIR_WITH_GIT = Path.Combine (TEMP_DIR, "with");
		
		public static List<string> TEST_DIRS = new List<string> {DIR_WO_GIT, DIR_WITH_GIT};
		
		public static void SetUp()
		{
			if (!Directory.Exists (TEMP_DIR)) {
				Directory.CreateDirectory (TEMP_DIR);	
			}
			
			TEST_DIRS.ForEach ( dir => Directory.CreateDirectory (dir) );
			if (!Repository.IsValid (DIR_WITH_GIT)) {
				Repository.Init (DIR_WITH_GIT);
			}
		}
		
		public static void TearDown ()
		{
			try {
				TEST_DIRS.Where ( dir => Directory.Exists (dir)).ToList ().ForEach (d => Directory.Delete (d, true));
			} catch (Exception ex) {
					
			}
			
		}
	}
}
