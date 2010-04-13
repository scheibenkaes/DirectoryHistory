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
using System.Linq;
using Gtk;

using Mono.Unix;

using DirectoryHistory;
using DirectoryHistory.History;

namespace DirectoryHistory.UI
{


	[System.ComponentModel.ToolboxItem(true)]
	public partial class FolderList : Gtk.Bin
	{
		private TreeStore treeStore;
		
		public event EventHandler<FileSelectedEventArgs> OnFileSelected;

		public FolderList ()
		{
			this.Build ();
			InitializeTreeStore ();
			
			treeview.AppendColumn (Catalog.GetString ("Status"), new CellRendererPixbuf (), "icon_name", 0);
			treeview.AppendColumn (Catalog.GetString ("File"), new CellRendererText (), "text", 1);
			
			RegisterCallbacks ();
		}

		private void RegisterCallbacks ()
		{
			treeview.Selection.Changed += HandleTreeviewSelectionChanged;
		}

		private void HandleTreeviewSelectionChanged (object sender, EventArgs e)
		{
			var selectedFile = ReadSelection (sender);
			
			if (OnFileSelected != null) {
				OnFileSelected (this, new FileSelectedEventArgs (selectedFile));
			}
		}
		
		private string ReadSelection (object sender)
		{
			TreeIter iter;
			TreeModel model;
			
			if (((TreeSelection)sender).GetSelected (out model, out iter)) 
			{
				return (string) model.GetValue (iter, 1);
			}
			return string.Empty;
		}
		
		

		private void InitializeTreeStore ()
		{
			treeStore = new TreeStore (typeof(string), typeof(string));
			treeview.Model = treeStore;
		}

		public void OnDirectoryUpdated (object sender, DirectoryStatusWasUpdatedEventArgs args)
		{
			InitializeTreeStore ();
			
			DisplayAsRootFolder (args.DirectoryThatChanged);
		}

		private void DisplayAsRootFolder (IDirectoryWithHistory root)
		{
			if (root == null)
				throw new ArgumentNullException ("root");
			
			AddDirectoryToList (root);
			
			AddContainingFiles (root);
			
			TreeIter treeIter;
			
			treeStore.GetIter (out treeIter, new TreePath ("0"));
			
			AddSubDirectories (treeIter, root);
		}

		private void AddContainingFiles (IDirectoryWithHistory directoryWithHistory)
		{
			var files = directoryWithHistory.ChildFiles;
			foreach (IFileWithHistory file in files) {
				treeStore.AppendValues (file.Status.GetStockFromFileStatus (), file.Path);
			}
		}

		private TreeIter AddDirectoryToList (IDirectoryWithHistory directoryWithHistory)
		{
			return treeStore.AppendValues (directoryWithHistory.Status.GetStockFromFileStatus (), directoryWithHistory.Path);
		}

		private void AddSubDirectories (TreeIter iter, IDirectoryWithHistory directory)
		{
			foreach (var child in directory.ChildDirectories) {
				AddDirectoryToList (child);
				AddContainingFiles (child);
				AddSubDirectories (iter, child);
			}
		}
	}
}
