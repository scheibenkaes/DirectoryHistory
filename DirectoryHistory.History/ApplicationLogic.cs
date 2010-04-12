//  
//  History.cs
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

namespace DirectoryHistory.History
{
	public delegate bool AskUserForCreation (string path);

	/// <summary>
	/// Main application object
	/// </summary>
	public class ApplicationLogic
	{
		public event EventHandler<DirectoryStatusWasUpdatedEventArgs> OnDirectoryLoaded;

		public event AskUserForCreation OnUserRequestForCreation;

		public IHistoryProvider HistoryProvider { get; private set; }

		public IDirectoryWithHistory RootDirectory { get; private set; }

		public void LoadDirectory (string path)
		{
			if (string.IsNullOrEmpty (path)) {
				throw new ArgumentNullException ("path");
			}
			
			if (!HistoryProvider.IsARepository (path)) {
				bool shouldCreate = AskUserIfHeWantsARepositoryToBeCreated (path);
				if (shouldCreate) {
					RootDirectory = HistoryProvider.CreateRepository (path);
				} else {
					return;
				}
			}
			LoadExistingRepository (path);
		}

		private bool AskUserIfHeWantsARepositoryToBeCreated (string path)
		{
			return OnUserRequestForCreation (path);
		}


		private void LoadExistingRepository (string path)
		{
			RootDirectory = HistoryProvider.LoadDirectory (path);
			if (OnDirectoryLoaded != null) {
				OnDirectoryLoaded (this, new DirectoryStatusWasUpdatedEventArgs (RootDirectory));
			}
		}
		
		public void CleanUp()
		{
			if (HistoryProvider != null)
				HistoryProvider.Dispose ();
		}

		public ApplicationLogic (IHistoryProvider provider)
		{
			HistoryProvider = provider;
		}
	}
}
