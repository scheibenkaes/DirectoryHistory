//  
//  TempFileCache.cs
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
using System.Collections.Generic;


namespace DirectoryHistory.History
{
	public struct CacheKey
	{
		public IFileWithHistory File {
			get;
			private set;
		}
		
		public IFileVersion Version  {
			get;
			private set;
		}
		
		public override bool Equals (object obj)
		{
			if (obj == null) {
				return false;
			}
			
			if (object.ReferenceEquals (this, obj)) {
				return true;
			}
			
			if (obj is CacheKey) {
				var other = (CacheKey) obj;
				return other.File.Path == File.Path && other.Version.ID == Version.ID;
			}
			return false;
		}
		
		
		public CacheKey (IFileWithHistory file, IFileVersion version)
		{
			File = file;
			Version = version;
		}
		
		public override int GetHashCode ()
		{
			return Version.ID.GetHashCode () + File.Path.GetHashCode ();
		}
	}
}
