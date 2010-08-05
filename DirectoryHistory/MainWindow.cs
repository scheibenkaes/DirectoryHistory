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
using System.Linq;

using Gtk;

using Mono.Unix;

using DirectoryHistory;
using DirectoryHistory.Common;
using DirectoryHistory.History;
using DirectoryHistory.UI;

public partial class MainWindow : Gtk.Window
{
	private ApplicationLogic logic;
	
	
	public MainWindow (ApplicationLogic logic) : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		this.logic = logic;
		logic.OnDirectoryLoaded += folderlist.OnDirectoryUpdated;
		logic.OnDirectoryLoaded += OnDirectoryLoaded;
		logic.OnUserRequestForCreation += AskUserForRepositoryCreation;
		
		folderlist.OnFileSelected += HandleFolderlistOnFileSelected;
		
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

	private void HandleFolderlistOnFileSelected (object sender, FileSelectedEventArgs e)
	{
		EnableAvailableActions (e.SelectedFile);
	}

	private void EnableAvailableActions (string selectedFile)
	{
		if (string.IsNullOrEmpty (selectedFile) ) {
			DisableAllFileActions ();
		} else {
			EnableActionsDependingOnTheSelectedFile (selectedFile);
		}
	}

	private void EnableActionsDependingOnTheSelectedFile (string selectedFile)
	{
		// TODO make this calculation take place in logic component
		var file = logic.HistoryProvider.GetFileOrDirectory (selectedFile);
		
		switch (file.Status) {
		case FileStatus.NotUnderVersionControl:
			addAction.Sensitive = true;
			fileHistoryAction.Sensitive = false;
			applyAction.Sensitive = false;
			break;
		case FileStatus.Committed:
			addAction.Sensitive = false;
			fileHistoryAction.Sensitive = file is IDirectoryWithHistory ? false : true;
			applyAction.Sensitive = false;
			break;
		case FileStatus.Changed:
			addAction.Sensitive = false;
			fileHistoryAction.Sensitive = file is IDirectoryWithHistory ? false : true;
			applyAction.Sensitive = true;
			break;
		default:
			throw new HistoryException (string.Format ("Unknown status {0}", file.Status));
		}
	}


	private void DisableAllFileActions ()
	{
		addAction.Sensitive = false;
		fileHistoryAction.Sensitive = false;
		applyAction.Sensitive = false;
	}


	private void ExitApp ()
	{
		logic.CleanUp ();
		Application.Quit ();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		ExitApp ();
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
		var dialog = new Dialog (Catalog.GetString ("Please decide"), this, DialogFlags.Modal);
		dialog.VBox.PackStart (new Label (string.Format (Catalog.GetString ("ID_AskUserForCreationOfARepository"), path)));
		dialog.AddButton (Catalog.GetString ("Yes"), ResponseType.Yes);
		dialog.AddButton (Catalog.GetString ("No"), ResponseType.No);
		
		bool yesNo = false;
		dialog.ShowAll ();
		if (dialog.Run () == (int)ResponseType.Yes) {
			yesNo = true;
		}
		dialog.Destroy ();
		return yesNo;
	}


	protected virtual void OnRefreshActionActivated (object sender, System.EventArgs e)
	{
		logic.ExceptionHandling.RunActionSavely (() =>
		{
			if (logic.RootDirectory != null) {
				logic.LoadDirectory (logic.RootDirectory.Path);
			}
		});
	}


	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		logic.ExceptionHandling.RunActionSavely (() => { ExitApp (); });
	}

	void ShowInfoDialog ()
	{
		var about = new AboutDialog { Authors = new[] { "Benjamin Klüglein <scheibenkaes@googlemail.com>" }, ProgramName = Catalog.GetString ("Directory History"), License = LicenseText, Comments = Catalog.GetString ("A program to keep a simple history of your files."), Title = Catalog.GetString ("About Directory History") };
		
		about.Run ();
		about.Destroy ();
	}

	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
	{
		logic.ExceptionHandling.RunActionSavely (() => { ShowInfoDialog (); });
	}

	protected virtual void OnAddActionActivated (object sender, System.EventArgs e)
	{
		var file = folderlist.ReadSelectedFile ();
		logic.HistoryProvider.AddFile (logic.HistoryProvider.GetFileOrDirectory (file));
		refreshAction.Activate ();
	}

	protected virtual void OnApplyActionActivated (object sender, System.EventArgs e)
	{
		var selected = folderlist.ReadSelectedFile ();
		var commitDialog = new CommitDialog (selected);
		if (commitDialog.Run () == (int)ResponseType.Ok) {
			var comment = commitDialog.Comment;
			
			Commit (selected, comment);
		}
		commitDialog.Destroy ();
		refreshAction.Activate ();
	}

	private void Commit (string selected, string comment)
	{
		logic.HistoryProvider.CommitChanges (logic.CreateCommit (selected, comment));
	}


	protected virtual void OnFileHistoryActionActivated (object sender, System.EventArgs e)
	{
		logic.ExceptionHandling.RunActionSavely (() =>
		{
			var selectedFile = folderlist.ReadSelectedFile ();
			if (!string.IsNullOrEmpty (selectedFile)) {
				var file = logic.HistoryProvider.GetFileOrDirectory (selectedFile);
				if (file is IFileWithHistory) {
					var historyDialog = new HistoryDialog (logic, file) { Modal = true };
					historyDialog.Show ();
				}
			}
		});
	}

	

	private const string LicenseText = " This program is free software: you can redistribute it and/or modify\r\n it under the terms of the GNU General Public License as published by\r\n the Free Software Foundation, either version 3 of the License, or\r\n (at your option) any later version.\r\n\r\n This program is distributed in the hope that it will be useful,\r\n but WITHOUT ANY WARRANTY; without even the implied warranty of\r\n MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\n GNU General Public License for more details.\r\n\r\n You should have received a copy of the GNU General Public License\r\n along with this program.  If not, see <http://www.gnu.org/licenses/>.\r\n";
}
