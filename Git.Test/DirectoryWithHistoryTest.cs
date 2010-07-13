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
using NMock2;

using GitSharp;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;


namespace Git.Test
{
	[TestFixture]
	public class DirectoryWithHistoryTest : GitTestCase
	{

		private IHistoryProvider provider = new HistoryProvider ();
		private Mockery myMockery;

		[SetUp]
		public void SetUp ()
		{
			myMockery = new Mockery ();
			TestHelper.CreateTestRepo ();
		}

		[TearDown]
		public void TearDown ()
		{
			myMockery.Dispose ();
			myMockery = null;
			TestHelper.RemoveTestRepo ();
		}

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
			TestHelper.CreateTestRepo ();
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
		public void IsRootDirectory_IsSetWhenRoot ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			Assert.IsTrue (provider.GetDirectory (TestData.TEMP_DIR).IsRootDirectory);
		}
		
		#region IsNotRootDirectory_WhenNotRoot
		public void Setup_IsNotRootDirectory_WhenNotRoot ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			TestHelper.CreateDirectory (Path.Combine (TestData.TEMP_DIR, "committed_dir"));
		}
		
		[Test]
		public void IsNotRootDirectory_WhenNotRoot ()
		{
			TestHelper.CreateTestRepo ();
			Setup_IsNotRootDirectory_WhenNotRoot ();
			Assert.IsFalse (provider.GetDirectory (TestData.TEMP_DIR.PathCombine ("committed_dir")).IsRootDirectory);
		}
		#endregion

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetContentForVersion_IsNotAValidOperation ()
		{
			TestHelper.CreateTestRepo ();
			provider.LoadDirectory (TestData.TEMP_DIR);
			provider.GetDirectory ("/tmp/test_repo").GetContentForVersion (myMockery.NewMock<IFileVersion> ());
		}
		
		[Test]
		public void Test_Why_FoldersAppearToBeNotUnderVC ()
		{
			TestHelper.CreateTestRepo ();
			provider.LoadDirectory (TestData.TEMP_DIR);
			var dirname = TestData.TEMP_DIR.PathCombine ("test_dir");
			
			Directory.CreateDirectory (TestData.TEMP_DIR.PathCombine (dirname));
			var filename = dirname.PathCombine ("foo.php");
			CreateFile (filename);
			
			var commit = new DirectoryHistory.History.Commit (provider.GetDirectory (dirname), "egal");
			
			provider.CommitChanges (commit);
			
			var dir = provider.GetDirectory (dirname);
			
			Assert.AreEqual (FileStatus.Committed, dir.Status);
		}
		
		[Test]
		public void IfADirContainsAChangedFile_StatusShouldBe_Changed ()
		{
			TestHelper.CreateTestRepo ();
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			var dirname = TestData.TEMP_DIR.PathCombine ("test_dir");
			
			Directory.CreateDirectory (TestData.TEMP_DIR.PathCombine (dirname));
			var filename = dirname.PathCombine ("foo.php");
			CreateFile (filename);
			
			var commit = new DirectoryHistory.History.Commit (provider.GetDirectory (dirname), "egal");
			
			provider.CommitChanges (commit);
			
			var dir = provider.GetDirectory (dirname);
			
			File.AppendAllText (filename, "\nfoooooo");
			
			Assert.AreEqual (FileStatus.Changed, dir.Status);
		}
	}
}
