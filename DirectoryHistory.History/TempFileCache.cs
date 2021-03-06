//  
//  TempFileCache.cs
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
using System.Collections.Generic;

namespace DirectoryHistory.History
{
	public class TempFileCache
	{
		private readonly ITempFileCreator creator;
		
		private IDictionary<CacheKey,string> Cache  {
			get;
			set;
		}
		
		public string GetFile (IFileWithHistory file, IFileVersion version)
		{
			if (file == null || version == null) {
				throw new ArgumentNullException ("file and version must not be null");
			}
			string cachedFile;
			var key = new CacheKey (file, version);
			if (Cache.ContainsKey (key)) {
				cachedFile = Cache[key];
			}
			else {
				var tempFile = creator.CreateTempFileFromVersion (file, version);
				Cache[key] = tempFile;
				cachedFile = tempFile;
			}
			return cachedFile;
		}
		
		public TempFileCache (ITempFileCreator creator)
		{
			Cache = new Dictionary<CacheKey, string> ();
			this.creator = creator;
		}
	}
}

