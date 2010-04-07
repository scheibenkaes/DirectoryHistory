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
			provider = null;
		}
		
		[Test]
		public void AddsAFileToTheRepository ()
		{
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
