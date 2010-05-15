//  
//  ExceptionHandlingTest.cs
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

using DirectoryHistory;

namespace History.Test
{
	[TestFixture()]
	public class ExceptionHandlingTest
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
		[ExpectedException(typeof(ArgumentNullException))]
		public void Throws_Exception_If_Displayer_Is_Null ()
		{
			new ExceptionHandling (null);
		}
		
		[Test]
		public void Should_Invoke_Its_Displayer ()
		{
			IExceptionDisplayer displayer = myMockery.NewMock <IExceptionDisplayer> ();
			
			var exceptionHandling = new ExceptionHandling (displayer);
			
			var exception = new Exception ("Uupps");
			
			Expect.Once.On (displayer).Method ("DisplayException").With (exception);
			
			exceptionHandling.DisplayException (exception);
		}
	}
}

