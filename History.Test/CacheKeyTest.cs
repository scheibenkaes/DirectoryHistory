//  
//  CacheKeyTest.cs
//  
//  Author:
//       bkn <${AuthorEmail}>
// 
//  Copyright (c) 2010 bkn
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
	public class CacheKeyTest
	{
		private Mockery myMockery;
		
		private IFileWithHistory file1;
		private IFileVersion version1;
		
		private IFileWithHistory file2;
		private IFileVersion version2;
		
		[SetUp]
		public void SetUp ()
		{
			myMockery = new Mockery ();
			file1 = myMockery.NewMock<IFileWithHistory> ();
			Stub.On (file1).GetProperty ("Path").Will (Return.Value ("/tmp/foooooo"));
			version1 = myMockery.NewMock<IFileVersion> ();
			Stub.On (version1).GetProperty ("ID").Will (Return.Value ("foooooo"));
			
			file2 = myMockery.NewMock<IFileWithHistory> ();
			Stub.On (file2).GetProperty ("Path").Will (Return.Value ("/tmp/baaaaaaaaaarrrrrr"));
			version2 = myMockery.NewMock<IFileVersion> ();
			Stub.On (version2).GetProperty ("ID").Will (Return.Value ("baaaaaaaaarrrrrrrrrr"));
		}
		
		[TearDown]
		public void TearDown ()
		{
			myMockery.Dispose ();
			myMockery = null;
		}
		
		[Test()]
		public void Equals_ReturnsTrue_IfFileAndVersionAreEqual ()
		{
			Assert.IsTrue (new CacheKey (file1, version1).Equals (new CacheKey (file1, version1)));
		}
		
		[Test()]
		public void Equals_ReturnsFalse_IfFilesAreNotEqual ()
		{
			Assert.IsFalse (new CacheKey (file1, version1).Equals (new CacheKey (file2, version1)));
		}
		
		[Test()]
		public void Equals_ReturnsFalse_IfVersionsAreNotEqual ()
		{
			Assert.IsFalse (new CacheKey (file1, version1).Equals (new CacheKey (file1, version2)));
		}
		
		[Test()]
		public void GetHashCode_Differs_IfFileIsDifferent ()
		{
			Assert.AreNotEqual (new CacheKey (file1, version1).GetHashCode (), new CacheKey (file2, version1).GetHashCode ());
		}	
		
		[Test()]
		public void GetHashCode_Differs_IfVersionIsDifferent ()
		{
			Assert.AreNotEqual (new CacheKey (file1, version1).GetHashCode (), new CacheKey (file1, version2).GetHashCode ());
		}	
		
		[Test()]
		public void GetHashCode_IsEqual_IfVersionAndFileAreNotDifferent ()
		{
			Assert.AreEqual (new CacheKey (file1, version1).GetHashCode (), new CacheKey (file1, version1).GetHashCode ());
		}	
	}
}

