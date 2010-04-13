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
	public event EventHandler<FileSelectedEventArgs> OnFileSelected;
	
	private ApplicationLogic logic;

	public MainWindow (ApplicationLogic logic) : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		this.logic = logic;
		logic.OnDirectoryLoaded += folderlist.OnDirectoryUpdated;
		logic.OnDirectoryLoaded += OnDirectoryLoaded;
		logic.OnUserRequestForCreation += AskUserForRepositoryCreation;
		
		refreshAction.Sensitive = false;
		addAction.Sensitive = false;
		applyAction.Sensitive = false;
		fileHistoryAction.Sensitive = false;
	}

	private void OnDirectoryLoaded (object sender, DirectoryStatusWasUpdatedEventArgs args)
	{
		loadedDirectoryLabel.Text = args.DirectoryThatChanged.Path;
		
		refreshAction.Sensitive = true;
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
			logic.LoadDirectory (fileChooser.Filename ?? fileChooser.CurrentFolder);
		}
		fileChooser.Destroy ();
	}

	private bool AskUserForRepositoryCreation (string path)
	{
		var dialog = new Dialog (Catalog.GetString ("There is not yet a repository at this position."), this, DialogFlags.Modal);
		
		dialog.VBox.PackStart (new Label (string.Format (Catalog.GetString ("Should a new repository be created at {0}?\n"), path)));
		dialog.AddButton (Catalog.GetString ("Yes"), ResponseType.Yes);
		dialog.AddButton (Catalog.GetString ("No"), ResponseType.No);
		
		bool yesNo = false;
		dialog.ShowAll ();
		if (dialog.Run () == (int) ResponseType.Yes)
		{
			yesNo = true;
		}
		dialog.Destroy ();
		return yesNo;
	}


	protected virtual void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		if (logic.RootDirectory != null) {
			logic.LoadDirectory (logic.RootDirectory.Path);
		}
	}


	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		logic.CleanUp ();
		Application.Quit ();
	}

	void ShowInfoDialog ()
	{
		var about = new AboutDialog { Authors = new[] { "Benjamin Klüglein <scheibenkaes@googlemail.com>" }, ProgramName = "Directory History", License = LicenseText, Comments = Catalog.GetString ("A program to keep a simple history of your files."), Title = Catalog.GetString ("About Directory History") };
		
		about.Run ();
		about.Destroy ();
	}

	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
	{
		ShowInfoDialog ();
	}

	protected virtual void OnAddActionActivated (object sender, System.EventArgs e)
	{
	}	
	
	protected virtual void OnApplyActionActivated (object sender, System.EventArgs e)
	{
	}
	
	protected virtual void OnFileHistoryActionActivated (object sender, System.EventArgs e)
	{
	}
	
	
	
	private const string LicenseText = " This program is free software: you can redistribute it and/or modify\r\n it under the terms of the GNU General Public License as published by\r\n the Free Software Foundation, either version 3 of the License, or\r\n (at your option) any later version.\r\n\r\n This program is distributed in the hope that it will be useful,\r\n but WITHOUT ANY WARRANTY; without even the implied warranty of\r\n MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\n GNU General Public License for more details.\r\n\r\n You should have received a copy of the GNU General Public License\r\n along with this program.  If not, see <http://www.gnu.org/licenses/>.\r\n";
}
