//  
//  TempFileCreatorTest.cs
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
using System.Linq;
using NUnit.Framework;
using NMock2;

using DirectoryHistory.History;
using DirectoryHistory.History.Git;

namespace Git.Test
{
	[TestFixture()]
	public class TempFileCreatorTest
	{
		private Mockery myMockery;
		
		private IFileVersion version1;
		private IFileWithHistory file1;
		
		[SetUp]
		public void SetUp ()
		{	
			myMockery = new Mockery ();
			file1 = myMockery.NewMock <IFileWithHistory> ();
			version1 = myMockery.NewMock<IFileVersion> ();
			
			var testFile = "/tmp/test_repo/use_cases.odt";
			//Stub.On (file1).GetProperty ("Path").Will (Return.Value(testFile));
		}
		
		[TearDown]
		public void TearDown ()
		{
			myMockery.Dispose ();
			myMockery = null;
		}
		
		[Test]
		public void Implements_ITempFileCreator ()
		{
			Assert.IsInstanceOfType (typeof (ITempFileCreator), new TempFileCreator ());
		}
		
		[Test]
		public void Creates_ATempFile ()
		{
			byte[] content = new [] {0, 0, 0}.ToList ().ConvertAll<byte> (b => Convert.ToByte (b)).ToArray ();
			
			Stub.On (file1).Method ("GetContentForVersion").Will (Return.Value (content));
			
			var creator = new TempFileCreator ();
			var createdFile = creator.CreateTempFileFromVersion (file1, version1);
			
			Assert.IsTrue (File.Exists (createdFile));
		}
		
		[Test]
		public void CreatedTempFile_HasSameEndingAsTheOriginalFile ()
		{
			Assert.Fail ();
		}
	}
}

