//  
//  DirectoryWithHistoryTest.cs
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
using NUnit.Framework;

using GitSharp;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;


namespace Git.Test
{
	[TestFixture()]
	public class DirectoryWithHistoryTest: GitTestCase
	{
		private DirectoryWithHistory directory;
		
		private DirectoryInfo tmpDir;
		
		private IHistoryProvider provider = new HistoryProvider ();

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_PathIsNecessary ()
		{
			new DirectoryWithHistory (new HistoryProvider (), null);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ProviderIsNecessary ()
		{
			new DirectoryWithHistory (null, "/tmp");
		}
		
		[Test]
		public void Status_ShouldBe_NotUnderVC_WhenCreated ()
		{
			var dir = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			
			string dirPath = TestData.DIR_WITH_GIT.PathCombine ("testdir");
			Directory.CreateDirectory (dirPath);			
			
			Assert.AreEqual (FileStatus.NotUnderVersionControl, dir.Status);
		}
	}
}
