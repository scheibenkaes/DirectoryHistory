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

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
	{		
		var fileChooser = new FileChooserDialog ("Open directory", this, FileChooserAction.Open, Stock.Cancel, ResponseType.Cancel, Stock.Open, ResponseType.Close);
		FileFilter filter = new FileFilter ();
		filter.AddCustom (FileFilterFlags.Filename, info => Directory.Exists (info.Filename));
		fileChooser.Filter = filter;
		if (fileChooser.Run () == (int)ResponseType.Close) {
			Console.WriteLine (fileChooser.Filename);
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
	
	
	
	
}
