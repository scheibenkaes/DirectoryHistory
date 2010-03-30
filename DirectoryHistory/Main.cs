//  
//  Main.cs
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

using Mono.Unix;

using DirectoryHistory.History;

namespace DirectoryHistory
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			
			Catalog.Init ("i8n1", "locale");
			
			var git = new DirectoryHistory.History.Git.HistoryProvider ();
			
			var applLogic = new ApplicationLogic (git);
			
			MainWindow win = new MainWindow (applLogic);
			win.Show ();
			Application.Run ();
		}
	}
}
