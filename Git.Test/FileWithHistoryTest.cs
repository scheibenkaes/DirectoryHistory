//  
//  FileWithHistoryTest.cs
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
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

using GitSharp;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{


	[TestFixture()]
	public class FileWithHistoryTest : GitTestCase
	{
		private HistoryProvider provider;

		[SetUp]
		public override void SetUp ()
		{
			base.SetUp ();
			provider = new HistoryProvider ();
		}

		[TearDown]
		public override void TearDown ()
		{
			base.TearDown ();
			provider.Dispose ();
			provider = null;
		}
		
		[Test]
		public void Test_PathInRepository()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file1 	= provider.GetFile ("/tmp/gittest/changed.txt");
			var file2 	= provider.GetFile ("/tmp/gittest/committed.txt");
			
			Assert.AreEqual ("changed.txt", file1.PathInRepository);
			Assert.AreEqual ("committed.txt", file2.PathInRepository);
			
		}

		[Test]
		public void Status_NotUnderVersionControll ()
		{
			//var repo = new Repository (TestData.DIR_WITH_GIT);
			var dir = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "Status_NotUnderVersionControll.txt");
			CreateFile (testFilePath);
			var file = new FileWithHistory (provider, testFilePath);
			
			Assert.AreEqual (FileStatus.NotUnderVersionControl, file.Status);
		}

		[Test]
		public void Status_Changed ()
		{
			//var repo = new Repository (TestData.DIR_WITH_GIT);
			var dir = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "changed.txt");
			CreateFile (testFilePath);
			var file = new FileWithHistory (provider, testFilePath);
			// TODO Test this over API not directly with git impl
			provider.Repository.Index.Add (file.PathInRepository);
			
			Assert.AreEqual (FileStatus.Changed, file.Status);
		}

		[Test]
		public void Status_Committed ()
		{
			//var repo = new Repository (TestData.DIR_WITH_GIT);
			var dir = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "committed.txt");
			CreateFile (testFilePath);
			var file = new FileWithHistory (provider, testFilePath);
			provider.Repository.Index.Add (file.PathInRepository);
			
			Assert.AreEqual (FileStatus.Changed, file.Status);
			
			provider.Repository.Index.CommitChanges ("Heureka", new Author ("sad", "sad@boo.de"));
			provider.Dispose ();
			
			Assert.AreEqual (FileStatus.Commited, file.Status);
		}
		
		[Test]
		public void File_ProvidesHistory()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file2 	= provider.GetFile ("/tmp/test_repo/with_2_versions.txt");
			
			Assert.AreEqual (2, file2.History.Count ());
		}
	}
}
