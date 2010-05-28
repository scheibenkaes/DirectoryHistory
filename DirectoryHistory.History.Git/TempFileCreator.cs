//  
//  TempFileCreator.cs
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

using DirectoryHistory.History;

namespace DirectoryHistory.History.Git
{
	public class TempFileCreator : ITempFileCreator
	{
		public IHistoryProvider Provider {
			private set;
			get;
		}
		
		public string CreateTempFileFromVersion (IFileWithHistory file, IFileVersion version)
		{
			var temp = Path.GetTempFileName ();
			var content = file.GetContentForVersion (version);
			WriteContentToTempFile (content, temp);
			var completeTempFileName = AppendOriginalFileEnding (temp, ReadOriginalFileEnding (file.PathInRepository));
			File.Move (temp, completeTempFileName);
			return completeTempFileName; 
		}

		private static string ReadOriginalFileEnding (string filename)
		{
			return filename.Split ('.').ToList ().LastOrDefault ();
		}

		
		private static string AppendOriginalFileEnding (string file, string fileEnding)
		{
			if (file.EndsWith (fileEnding)) {
				return file;
			}
			return string.Format ("{0}.{1}", file, fileEnding);
		}

		private static void WriteContentToTempFile (System.Byte[] content, string file)
		{
			using (var outFile = File.OpenWrite (file)) {
				foreach (var b in content) {
					outFile.WriteByte (b);
				}
			}			
		}
		
		public TempFileCreator (IHistoryProvider provider)
		{
			Provider = provider;
		}

	}
}

