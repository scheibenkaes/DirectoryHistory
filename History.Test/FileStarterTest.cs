//  
//  FileStarterTest.cs
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

using DirectoryHistory.Common;

namespace History.Test
{
	[TestFixture()]
	public class FileStarterTest
	{
		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void Throws_Argument_NullException_WhenCalled_WithNull ()
		{
			FileStarter.StartFile (null);
		}
		
		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void Throws_Argument_NullException_WhenCalled_WithEmptyStr ()
		{
			FileStarter.StartFile ("");
		}
		
		[Test]
		[ExpectedException(typeof (FileNotFoundException))]
		public void Throws_FileNotFound_FileNotExisting ()
		{
			FileStarter.StartFile ("yadadadadad.fooooo");
		}
	}
}

