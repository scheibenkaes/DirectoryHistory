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
using System.IO;
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

		[Test()]
		public void HowTo_DetectIfAFileIsAddedToGit ()
		{
			//Commands.GitDirectory = TestData.DIR_WITH_GIT;
			
			//Assert.IsNotNull (Commands.Repository);
			
			var repo = new Repository (TestData.DIR_WITH_GIT);
			
			Assert.IsTrue (Repository.IsValid (TestData.DIR_WITH_GIT));
			
			var testFile = File.CreateText (Path.Combine (TestData.DIR_WITH_GIT, "test.txt"));
			testFile.WriteLine ("test 123");
			testFile.Flush ();
			testFile.Close ();			
			
			repo.
			
			Assert.Fail ();
		}
	}
}
