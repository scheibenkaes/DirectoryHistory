//  
//  FolderList.cs
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
using Gtk;

using DirectoryHistory.History;

namespace DirectoryHistory.UI
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class FolderList : Gtk.Bin
	{
		private TreeStore treeStore;

		public FolderList ()
		{
			this.Build ();
			InitializeTreeStore ();
			
			treeview.AppendColumn ("Status", new CellRendererText (), "text", 0);
			treeview.AppendColumn ("File", new CellRendererText (), "text", 1);
		}

		private void InitializeTreeStore ()
		{
			treeStore = new TreeStore (typeof(string), typeof(string));
			treeview.Model = treeStore;
		}
		
		public void OnDirectoryUpdated (object sender, DirectoryStatusWasUpdatedEventArgs args)
		{
			Console.WriteLine ("update");
		}
	}
}
