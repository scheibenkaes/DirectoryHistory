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
using NMock2;

using GitSharp;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{


	[TestFixture()]
	public class FileWithHistoryTest : GitTestCase
	{
		private HistoryProvider provider;
		
		private Mockery myMockery;

		[SetUp]
		public override void SetUp ()
		{
			base.SetUp ();
			provider = new HistoryProvider ();
			
			myMockery = new Mockery ();
		}

		[TearDown]
		public override void TearDown ()
		{
			base.TearDown ();
			provider.Dispose ();
			provider = null;
			
			myMockery.Dispose ();
			myMockery = null;
		}
		
		[Test]
		public void Test_PathInRepository()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file1 	= provider.GetFile ("/tmp/test_repo/changed.txt");
			var file2 	= provider.GetFile ("/tmp/test_repo/committed.txt");
			
			Assert.AreEqual ("changed.txt", file1.PathInRepository);
			Assert.AreEqual ("committed.txt", file2.PathInRepository);
			
		}

		[Test]
		public void Status_NotUnderVersionControll ()
		{
			provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "Status_NotUnderVersionControll.txt");
			CreateFile (testFilePath);
			var file = new FileWithHistory (provider, testFilePath);
			
			Assert.AreEqual (FileStatus.NotUnderVersionControl, file.Status);
		}

		[Test(Description = "Test against the test repo")]
		public void Status_Changed ()
		{
			provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "changed.txt");
			var file = provider.GetFile (testFilePath);
			
			Assert.AreEqual (FileStatus.Changed, file.Status);
		}

		[Test(Description = "Point the code to a already existing repo and check the file there")]
		public void Status_Committed ()
		{
			provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFilePath = Path.Combine (TestData.DIR_WITH_GIT, "committed.txt");
			var file = provider.GetFile (testFilePath);
			
			Assert.AreEqual (FileStatus.Committed, file.Status);
		}
		
		[Test]
		public void History_OfAFileWith2Entries()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file2 	= provider.GetFile ("/tmp/test_repo/with_2_versions.txt");
			
			Assert.AreEqual (2, file2.History.Count ());
		}
		
		[Test]
		[Ignore("Maybe this isn't needed")]
		public void DetectsIfAFileIsBinary ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file2 	= provider.GetFile ("/tmp/test_repo/use_cases.odt");
			Assert.IsTrue (file2.IsBinaryFile ());
		}
		
		[Test]
		[Ignore("Maybe this isn't needed")]
		public void DetectsIfAFileIsNotBinary ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file2 	= provider.GetFile ("/tmp/test_repo/with_2_versions.txt");
			Assert.IsFalse (file2.IsBinaryFile ());
		}
		
		[Test]
		public void GetContentForVersion_ReadsContentsCorrectly ()
		{
			var longId = "cd3d0560e7bce3b07ad10f9a2c67a4a18b99ab26";
			IFileVersion version = myMockery.NewMock<IFileVersion> ();
			Stub.On (version).GetProperty ("ID").Will(Return.Value (longId));
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file  = provider.GetFile ("/tmp/test_repo/with_2_versions.txt");
			var content = file.GetContentForVersion (version);
			
			var expectedContent = @"asd

asf
";
			
			Assert.AreEqual (expectedContent, content);
		}
	}
}
