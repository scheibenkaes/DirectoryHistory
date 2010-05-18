//  
//  FileSystemTests.cs
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
using System.IO;
using System.Diagnostics;

using Git.Test;

namespace History.Test
{
	[TestFixture()]
	public class FileSystemTests
	{
		[Test]
		public void OpenExistingFilesWithAssociatedPrograms ()
		{
			// Works
			//Process.Start (Path.Combine(TestData.TEMP_DIR, "use_cases.odt"));
			
		}
	}
}

