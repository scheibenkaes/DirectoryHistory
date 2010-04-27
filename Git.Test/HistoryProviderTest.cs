//  
//  HistoryProviderTest.cs
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
	public class HistoryProviderTest : GitTestCase
	{
		private HistoryProvider provider;
		
		[SetUp]
		public override void SetUp ()
		{
			base.SetUp ();
			provider = new HistoryProvider ();
		}
		
		[TearDown]
		public override void TearDown()
		{
			base.TearDown ();
			provider.Dispose ();
			provider = null;
		}
		
		[Test]
		public void GitCommitting()
		{
			var testFile = TestData.DIR_WITH_GIT.PathCombine ("GitCommitting.txt");
			
			var repo = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			CreateFile (testFile);
			
			var file = provider.GetFile (testFile);
			provider.AddFile (file);
			Assert.AreEqual (FileStatus.Changed, file.Status);
			
			var commit = new Commit (file, "GitCommitting TestCase");
			
			provider.CommitChanges (commit);
			
			Assert.AreEqual (FileStatus.Committed, file.Status);
		}
		
		[Test]
		public void GitCommit_EffectsOnlySelectedFiles()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			var testFile_not2BeCommitted 	= "/tmp/test_repo/changed2.txt";
			var testFile 					= "/tmp/test_repo/changed.txt";
			
			var commit = new Commit (provider.GetFile (testFile), "GitCommit_EffectsOnlySelectedFiles");
			provider.CommitChanges (commit);
			
			Assert.AreEqual (FileStatus.Committed, provider.GetFile (testFile).Status, 				"Committed file is not committed");
			Assert.AreEqual (FileStatus.Changed, provider.GetFile (testFile_not2BeCommitted).Status, "NOT committed file is not changed");
		}
		
		[Test]
		public void Test_GetFile()
		{
			provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFile = TestData.DIR_WITH_GIT.PathCombine ("committed.txt");
			
			var file = provider.GetFile (testFile);
			
			Assert.AreEqual (testFile, file.Path);
		}
		
		[Test]
		public void AddsAFileToTheRepository ()
		{
			provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFile = TestData.DIR_WITH_GIT.PathCombine ("adding.txt");
			Console.WriteLine (testFile);
			CreateFile (testFile);
			
			var file = new FileWithHistory (provider, testFile);
			
			provider.AddFile (file);
			
			Assert.IsNotNull (file.Status);
			Assert.AreEqual (FileStatus.Changed, file.Status);
		}
		
		[Test]
		public void IsAIDisposable ()
		{
			Assert.IsTrue (provider is IDisposable);
		}
		
		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void TryingToAccessANotExistingFileLeadsToAnError ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var file2 	= provider.GetFile ("/tmp/test_repo/i_do_NOT_exist.txt");
		}
		
		[Test]
		public void Test_GetDirectory ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var path = TestData.TEMP_DIR.PathCombine ("committed_dir");
			
			var dir = provider.GetDirectory (path);
			Assert.IsNotNull (dir);
		}
		
		[Test]
		public void GetFileOrDirectory_ReturnsADirectoryIfExisting()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var path = TestData.TEMP_DIR.PathCombine ("committed_dir");
			
			var dir = provider.GetFileOrDirectory (path);
			Assert.IsNotNull (dir);
			Assert.IsInstanceOfType (typeof (IDirectoryWithHistory), dir);
		}
		
		[Test]
		public void GetFileOrDirectory_ReturnsAFileIfExisting()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var path = TestData.DIR_WITH_GIT.PathCombine ("committed.txt");
			
			var dir = provider.GetFileOrDirectory (path);
			Assert.IsNotNull (dir);
			Assert.IsInstanceOfType (typeof (IFileWithHistory), dir);
		}
		
		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void GetFileOrDirectory_ThrowsFileNotFound()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var path = TestData.DIR_WITH_GIT.PathCombine ("not_existing_xxx.txt");
			
			var dir = provider.GetFileOrDirectory (path);
		}
		
		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void GetFileOrDirectory_ThrowsFileNotFound_WhenNullGiven()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			
			var dir = provider.GetFileOrDirectory (null);
		}
	}
}
