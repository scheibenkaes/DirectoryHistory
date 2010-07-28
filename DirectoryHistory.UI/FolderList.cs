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
using System.Collections.Generic;
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
			
		}

		private void RegisterCallbacks ()
		{
			treeview.Selection.Changed += HandleTreeviewSelectionChanged;
		}

		private void HandleTreeviewSelectionChanged (object sender, EventArgs e)
		{
			var selectedFile = ReadSelectedFile ();
			
			if (OnFileSelected != null) {
				OnFileSelected (this, new FileSelectedEventArgs (selectedFile));
			}
		}
		
		public string ReadSelectedFile ()
		{
			TreeIter iter;
			TreeModel model;
			if (treeview.Selection.GetSelected (out model, out iter)) 
			{
				return (string) model.GetValue (iter, 2);
			}
			return string.Empty;
		}

		private void InitializeTreeStore ()
		{
			treeStore = new TreeStore (typeof(string), typeof(string), typeof(string), typeof(string));
			treeview.Model = treeStore;
		}

		public void OnDirectoryUpdated (object sender, DirectoryStatusWasUpdatedEventArgs args)
		{
			InitializeTreeStore ();
			
			DisplayAsRootFolder (args.DirectoryThatChanged);
			
			treeview.ExpandAll ();
		}

		private void DisplayAsRootFolder (IDirectoryWithHistory root)
		{
			if (root == null)
				throw new ArgumentNullException ("root");
			
			InitializeTreeStore ();
			
			AppendColumns ();
			
			RegisterCallbacks ();
			
			treeview.FreezeChildNotify ();
			
			TreeIter treeIter;
			
			treeStore.GetIter (out treeIter, new TreePath ("0"));
			
			var subIter = AddRootDir (root);
			
			foreach (var dir in root.ChildDirectories) {
				AddDirectory (subIter, dir);
			}
			AddChildFiles (subIter, root.ChildFiles);
			
			treeview.ThawChildNotify ();
		}
		
		private void AppendColumns ()
		{
			treeview.AppendColumn (Catalog.GetString ("Type"), 		new CellRendererPixbuf (), 	"icon_name", 	0);
			treeview.AppendColumn (Catalog.GetString ("Status"), 	new CellRendererPixbuf (), 	"icon_name", 	1);
			treeview.AppendColumn (Catalog.GetString ("File"), 		new CellRendererText (), 	"text", 		2);
		}

		private TreeIter AddDirectory (TreeIter iter, IDirectoryWithHistory dir)
		{
			var childiter = AddASingleDirectory (iter, dir);
			foreach (var subdir in dir.ChildDirectories) {
				AddDirectory (childiter, subdir);
			}
			AddChildFiles (childiter, dir.ChildFiles);
			return childiter;
		}
		
		private void AddChildFiles (TreeIter iter, IEnumerable<IFileWithHistory> files)
		{
			foreach (var file in files) {
				AddASingleFile (iter, file);
			}
		}
		
		private void AddASingleFile (TreeIter iter, IFileWithHistory file)
		{
			treeStore.AppendValues (iter, 
				file.GetStockForType (),
				file.Status.GetStockFromFileStatus (), 
				System.IO.Path.GetFileName (file.Path),
				file.Path
				);
		}
		
		private TreeIter AddASingleDirectory (TreeIter iter, IDirectoryWithHistory dir)
		{
			return treeStore.AppendValues (iter, 
				dir.GetStockForType (),
				dir.Status.GetStockFromFileStatus (), 
				dir.PathInRepository,
				dir.Path
				);
		}
		
		private TreeIter AddRootDir (IDirectoryWithHistory dir)
		{
			return treeStore.AppendValues (
				dir.GetStockForType (),
				dir.Status.GetStockFromFileStatus (), 
				dir.Path);
		}
	}
}
