//  
//  HistoryDialog.cs
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
using System.Linq;

using DirectoryHistory.History;

namespace DirectoryHistory.UI
{
	public partial class HistoryDialog : Gtk.Dialog
	{
		public IFileWithHistory File  {
			get;
			private set;
		}
		
		public HistoryDialog (IFileWithHistory file)
		{
			this.Build ();
			File = file;
			
			label.Text = file.PathInRepository;
			
			var entries = CreateEntriesForFile (file);
			
			DisplayEntries (entries);
			
			ShowAll ();
		}

		private void DisplayEntries (IEnumerable<HistoryEntry> entries)
		{
			var entryBox = new Gtk.VBox ();
			entries.ToList ().ForEach (entryBox.Add);
			
			entriesVbox.Add (entryBox);
		}


		private static IEnumerable<HistoryEntry> CreateEntriesForFile (IFileWithHistory file)
		{
			foreach (var version in file.History) {
				yield return new HistoryEntry (version);
			}
			yield break;
		}

	}
}

