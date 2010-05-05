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
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			string dirPath = TestData.TEMP_DIR.PathCombine ("testdir_new");
			Directory.CreateDirectory (dirPath);			
			var dir = provider.GetDirectory (dirPath);
			Assert.AreEqual (FileStatus.NotUnderVersionControl, dir.Status);
		}
		
		[Test]
		public void Status_ShouldBe_NotUnderVC_WhenCreated_NotEmptyDirectory ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			string dirPath = TestData.TEMP_DIR.PathCombine ("testdir_new_not_empty");
			Directory.CreateDirectory (dirPath);			
			var containedFile = dirPath.PathCombine ("a_file.txt");
			CreateFile (containedFile);
			var dir = provider.GetDirectory (dirPath);
			Assert.AreEqual (FileStatus.NotUnderVersionControl, dir.Status);
			
			// Breaking the one assert per test rule here because of gits handling of empty files and the implementation taking care of this.
			// Impl. feels a bit dirty, so just to make sure ...
			Assert.AreEqual (FileStatus.NotUnderVersionControl, provider.GetFile (containedFile).Status);
		}
		
		[Test]
		public void IfNotUnderVC_TheStatusShouldBeAlwaysCommitted ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			Assert.Fail ("Need to implement getting of dirs first"); // 
		}
	}
}
