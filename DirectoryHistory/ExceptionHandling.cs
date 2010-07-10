//  
//  ExceptionHandling.cs
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
using GLib;

namespace DirectoryHistory
{
	public class ExceptionHandling
	{
		public ExceptionHandling ()
		{
			//GLib.ExceptionManager.UnhandledException += HandleGLibExceptionManagerUnhandledException;
		}

//		private void HandleGLibExceptionManagerUnhandledException (UnhandledExceptionArgs args)
//		{
//			DisplayException (args.ExceptionObject as Exception);
//		}
		
		public void DisplayException (Exception exception)
		{
			new ExceptionOccuredDialog ().DisplayException (exception);
		}
		
		public static void RunActionSavely (Action action)
		{
			try {
				action.Invoke ();
			} catch (Exception ex) {
				new ExceptionHandling ().DisplayException (ex);
			}
		}
	}
}

