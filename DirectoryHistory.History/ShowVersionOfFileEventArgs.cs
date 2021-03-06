//  
//  ShowVersionOfFileEventArgs.cs
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

namespace DirectoryHistory.History
{
	public class ShowVersionOfFileEventArgs : EventArgs
	{
		public IFileWithHistory File  {
			get;
			private set;
		}
		
		public IFileVersion Version  {
			get;
			private set;
		}
		
		public ShowVersionOfFileEventArgs (IFileWithHistory file, IFileVersion version)
		{
			if (file == null || version == null) {
				throw new ArgumentNullException ("File and version must not be null");
			}
			File = file;
			Version = version;
		}
	}
}

