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
	public class DirectoryWithHistoryTest
	{
		private DirectoryWithHistory directory;
		
		private DirectoryInfo tmpDir;
		
		private const string TEMP_DIR = "/tmp/gittest";
		
		private static readonly string DIR_WO_GIT = Path.Combine (TEMP_DIR, "wo");
		
		private static readonly string DIR_WITH_GIT = Path.Combine (TEMP_DIR, "with");
		
		private static List<string> TEST_DIRS = new List<string> {DIR_WO_GIT, DIR_WITH_GIT};
		
		private IHistoryProvider provider = new HistoryProvider ();
		
		[SetUp]
		public void SetUp()
		{
			if (!Directory.Exists (TEMP_DIR)) {
				tmpDir = Directory.CreateDirectory (TEMP_DIR);	
			}
			
			TEST_DIRS.ForEach ( dir => Directory.CreateDirectory (dir) );
			if (!Repository.IsValid (DIR_WITH_GIT)) {
				Repository.Init (DIR_WITH_GIT);
			}
			
		}
		
		[TearDown]
		public void TearDown()
		{
			TEST_DIRS.Where ( dir => Directory.Exists (dir)).ToList ().ForEach (d => Directory.Delete (d, true));
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
		public void FileStatusIsNotYetImplemented ()
		{
			Assert.AreEqual (new DirectoryWithHistory (provider, DIR_WITH_GIT).Status, FileStatus.NotUnderVersionControl);
		}
	}
}
