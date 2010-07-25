//  
//  ExtensionsTest.cs
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
using NUnit.Framework;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{


	[TestFixture()]
	public class ExtensionsTest
	{

		[Test()]
		public void Test_ReducePath ()
		{
			var proj = TestData.TEMP_DIR;
			var filename = Path.Combine ("foo", "bar.txt");
			var file = Path.Combine (proj, filename);
			
			Assert.AreEqual (filename, proj.ReducePath (file));
		}
		
		[Test]
		public void Test_ReducePath_Issue13 ()
		{
			var proj = TestData.TEMP_DIR;
			var file = Path.Combine (proj, "foo\\bar.txt");
			
			Assert.AreEqual ("foo\\bar.txt", proj.ReducePath (file));
		}
		
		[Test]
		public void Test_WrongReducing()
		{
			var file = Path.DirectorySeparatorChar + "tmp".PathCombine ( "gittest", "existing.txt");
			
			Assert.AreEqual ("existing.txt", "/tmp/gittest".ReducePath (file));
				
		}
			
	}
}
