//  
//  GitAPIPlayground.cs
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

namespace Git.Test
{


	[TestFixture()]
	public class GitAPIPlayground
	{
		private Repository repo;
		
		[SetUp]
		public void SetUp ()
		{
			TestData.SetUp ();
			
		}

		[TearDown]
		public void TearDown ()
		{
			TestData.TearDown ();
		}
		
//		private static File CreateFile (string path)
//		{
//			var file = File.CreateText (path);
//			return file;
//		}
		
		[Test()]
		public void HowTo_DetectIfAFileIsAddedToGit ()
		{
			var repo = new Repository (TestData.DIR_WITH_GIT);
			
			Assert.IsTrue (Repository.IsValid (TestData.DIR_WITH_GIT));
			var filePath = Path.Combine (TestData.DIR_WITH_GIT, "test.txt");
			var testFile = File.CreateText (filePath);
			testFile.WriteLine ("test 123");
			testFile.Flush ();
			testFile.Close ();				
			
			//var blob = Blob.CreateFromFile (repo, filePath);
			
			var status  = repo.Index.Status;
			
			repo.Index.Add (filePath);		
			
			Assert.IsTrue (status.Added.Contains ("test.txt"), "Added");
			Assert.IsTrue (status.Untracked.Contains ("asd.txt"), "Untracked");
			
			//Assert.Fail ();
			
		}
	}
}
