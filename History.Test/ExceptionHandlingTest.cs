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

using DirectoryHistory.Common;

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
		public void RunSavely_CatchesExceptions ()
		{
			var displayer = myMockery.NewMock<IExceptionDisplayer> ();
			var exceptionHandling = new ExceptionHandling (displayer);
			
			var exc = new Exception ();
			
			Expect.Once.On (displayer).Method ("DisplayException").With (exc);
			
			exceptionHandling.RunActionSavely (() =>
			{
				throw exc;
			});
		}
	}
}

