//  
//  MainWindow.cs
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

using Gtk;

using Mono.Unix;

using DirectoryHistory;
using DirectoryHistory.History;

public partial class MainWindow : Gtk.Window
{
	private ApplicationLogic logic;

	public MainWindow (ApplicationLogic logic) : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		this.logic = logic;
		logic.OnDirectoryLoaded += folderlist.OnDirectoryUpdated;
		logic.OnDirectoryLoaded += OnDirectoryLoaded;
	}
	
	private void OnDirectoryLoaded (object sender, DirectoryStatusWasUpdatedEventArgs args)
	{
		loadedDirectoryLabel.Text = args.DirectoryThatChanged.Path;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
	{
		var fileChooser = new FileChooserDialog (Catalog.GetString ("Open directory"), this, FileChooserAction.Open, Stock.Cancel, ResponseType.Cancel, Stock.Open, ResponseType.Close);
		FileFilter filter = new FileFilter ();
		filter.AddCustom (FileFilterFlags.Filename, info => Directory.Exists (info.Filename));
		fileChooser.Filter = filter;
		if (fileChooser.Run () == (int)ResponseType.Close) {
			logic.LoadDirectory (fileChooser.Filename);
		}
		fileChooser.Destroy ();
	}


	protected virtual void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		
	}


	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}

	void ShowInfoDialog ()
	{
		var about = new AboutDialog (){
			Authors = new [] {"Benjamin Klüglein <scheibenkaes@googlemail.com>"},
			ProgramName = "Directory History",
			License = LicenseText,
			Comments = Catalog.GetString ("A program to keep a simple history of your files."),
			Title = Catalog.GetString ("About Directory History")
		};
		
		about.Run ();
		about.Destroy ();
	}

	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
	{
		ShowInfoDialog ();
	}
	
	private const string LicenseText = @" This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
";
}
