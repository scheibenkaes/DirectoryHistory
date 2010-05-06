//  
//  MyCommitTest.cs
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

using NMock2;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{
	[TestFixture()]
	public class MyCommitTest : GitTestCase
	{
		private Mockery mockery;
		private HistoryProvider provider;

		[SetUp]
		public override void SetUp ()
		{
			base.SetUp ();
			mockery = new Mockery();
			provider = new HistoryProvider ();
		}

		[TearDown]
		public override void TearDown ()
		{
			base.TearDown ();
			mockery.Dispose ();
			mockery = null;
			
			provider.Dispose ();
			provider = null;
		}

		[Test()]
		public void Implements_ICommit ()
		{
			Assert.IsInstanceOfType (typeof(ICommit), new MyCommit ());
		}

		[Test]
		public void Creates_Commit_From_GitCommit ()
		{
			provider.LoadDirectory (TestData.TEMP_DIR);
			var git = new GitSharp.Commit (provider.Repository, "");
			var commit = MyCommit.FromGitCommit (git);
			Assert.IsNotNull (commit);
		}
	}
}

