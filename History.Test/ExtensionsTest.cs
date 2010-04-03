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
using NUnit.Framework;

using DirectoryHistory.History;

namespace History.Test
{


	[TestFixture()]
	public class ExtensionsTest
	{

		[Test()]
		public void Test_PathCombine ()
		{
			string p1 = "/foo/bar";
			string p2 = "ninja.pl";
			
			Assert.AreEqual (p1 + "/" + p2, p1.PathCombine (p2));
		}
	}
}
