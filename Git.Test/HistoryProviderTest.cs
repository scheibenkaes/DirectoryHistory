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
			
			Assert.AreEqual (FileStatus.Commited, file.Status);
		}
		
		[Test]
		public void GitCommit_EffectsOnlySelectedFiles()
		{
			var repo = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			
			var testFile = TestData.DIR_WITH_GIT.PathCombine ("adding.txt");
			var testFile_not2BeCommitted = TestData.DIR_WITH_GIT.PathCombine ("adding_no_commit.txt");
			
			CreateFile (testFile_not2BeCommitted);
			
			var file = provider.GetFile (testFile);
			var fileNotCommitted = provider.GetFile (testFile_not2BeCommitted);
			provider.AddFile (fileNotCommitted);
			var commit = new Commit (file, "GitCommitting TestCase");
			provider.CommitChanges (commit);
			
			Assert.AreEqual (FileStatus.Changed, fileNotCommitted.Status);
		}
		
		[Test]
		public void Test_GetFile()
		{
			var repo = provider.LoadDirectory (TestData.DIR_WITH_GIT);
			var testFile = TestData.DIR_WITH_GIT.PathCombine ("existing.txt");
			
			var file = provider.GetFile (testFile);
			
			Assert.AreEqual (testFile, file.Path);
		}
		
		[Test]
		public void AddsAFileToTheRepository ()
		{
			var repo = provider.LoadDirectory (TestData.DIR_WITH_GIT);
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
	}
}
