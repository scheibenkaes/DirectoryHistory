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
		public void GetFile_ReturnsTheCached_File ()
		{
			var creator = myMockery.NewMock<ITempFileCreator> ();
			
			var cache = new TempFileCache (creator);
			var file1 = myMockery.NewMock<IFileWithHistory> ();
			string filename = "/tmp/test_repo/use_cases.odt";
			
			Stub.On (creator).Method ("CreateTempFileFromVersion").Will (Return.Value (filename));
			
			Stub.On (file1).GetProperty ("Path").Will (Return.Value (filename));
			
			var file2 = myMockery.NewMock<IFileWithHistory> ();
			Stub.On (file2).GetProperty ("Path").Will (Return.Value (filename));
			
			var version = myMockery.NewMock<IFileVersion> ();
			Stub.On (version).GetProperty ("ID").Will (Return.Value ("0475abdb01ed08f8997986e319e2467b"));
			
			Assert.AreEqual (cache.GetFile (file1, version), cache.GetFile (file2, version));
		}

		[Test]
		public void GetFile_ReturnsDifferent_TempFile_IfVersionsDiffer ()
		{
			var creator = myMockery.NewMock<ITempFileCreator> ();
			
			var cache = new TempFileCache (creator);
			var file1 = myMockery.NewMock<IFileWithHistory> ();
			string filename = "/tmp/test_repo/use_cases.odt";
			
			
			
			Stub.On (file1).GetProperty ("Path").Will (Return.Value (filename));
			
			var version = myMockery.NewMock<IFileVersion> ();
			Stub.On (version).GetProperty ("ID").Will (Return.Value ("0475abdb01ed08f8997986e319e2467b"));
			var version2 = myMockery.NewMock<IFileVersion> ();
			Stub.On (version2).GetProperty ("ID").Will (Return.Value ("72311666ba461988b1264f6f5e368d17"));
			
			Stub.On (creator).Method ("CreateTempFileFromVersion").With (file1, version).Will (Return.Value (filename));
			Stub.On (creator).Method ("CreateTempFileFromVersion").With (file1, version2).Will (Return.Value (filename  + "2"));
			
			Assert.AreNotEqual (cache.GetFile (file1, version), cache.GetFile (file1, version2));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetFile_DoesntAccept_NullInFileParam ()
		{
			new TempFileCache (myMockery.NewMock<ITempFileCreator> ()).GetFile (null, myMockery.NewMock<IFileVersion> ());
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetFile_DoesntAccept_NullInFileVersionParam ()
		{
			new TempFileCache (myMockery.NewMock<ITempFileCreator> ()).GetFile (myMockery.NewMock<IFileWithHistory> (), null);
		}
	}
}

