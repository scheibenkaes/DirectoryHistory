//  
//  CommitTest.cs
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

namespace History.Test
{
	[TestFixture()]
	public class CommitTest
	{
		private Mockery mockery;
		
		[SetUp]
		public void SetUp()
		{
			mockery = new Mockery ();
		}
		
		[TearDown]
		public void TearDown()
		{
			mockery.Dispose ();
			mockery = null;
		}
		
		[Test()]
		public void CommitIsICommit ()
		{
			var file = mockery.NewMock<IFileWithHistory> ();
			Assert.IsTrue (new Commit (file, "Test") is ICommit);
		}
		
		
	}
}

