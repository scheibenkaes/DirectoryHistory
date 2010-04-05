//  
//  Extensions.cs
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
using System.IO;
using System.Linq;

namespace DirectoryHistory.History
{


	public static class Extensions
	{
		public static string PathCombine (this string p1, string p2)
		{
			return Path.Combine (p1, p2);
		}

		public static string PathCombine (this string p1, params string[] p2)
		{
			if (p2 == null || !p2.Any ()) {
				return p1;
			}
			var result = Path.Combine (p1, p2[0]);
			var rest = p2.ToList ();
			rest.RemoveAt (0);
			
			rest.ForEach (s => {
				result = Path.Combine (result, s);
			});
			
			return result;
		}
	}
}
