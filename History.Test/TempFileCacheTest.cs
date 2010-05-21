//  
//  TempFileCacheTest.cs
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
	public class TempFileCacheTest
	{
		private Mockery myMockery;
		
		[SetUp]
		public void SetUp ()
		{
			myMockery = new Mockery ();
		}
		
		[TearDown]
		public void TearDown ()
		{
			myMockery.Dispose ();
			myMockery = null;
		}
		
		[Test]
		public void IsASingleton ()
		{
			Assert.IsTrue (object.ReferenceEquals (TempFileCache.Instance, TempFileCache.Instance));
		}
		
		[Test]
		public void GetFile_ReturnsTheCached_File()
		{
			var cache = TempFileCache.Instance;
			var file1 = myMockery.NewMock<IFileWithHistory> ();
			string filename = "/tmp/test_repo/some_file.ppt";
			
			Stub.On (file1).GetProperty ("Path").Will (Return.Value (filename));
			
			var file2 = myMockery.NewMock<IFileWithHistory> ();
			Stub.On (file2).GetProperty ("Path").Will (Return.Value (filename));
			
			Assert.AreEqual (
			                 cache.GetFile (file1),
			                 cache.GetFile (file2)
			                 );
		}
	}
}

