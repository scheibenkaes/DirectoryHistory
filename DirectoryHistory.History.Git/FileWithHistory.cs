//  
//  FileWithHistory.cs
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

using GitSharp;

namespace DirectoryHistory.History.Git
{


	public class FileWithHistory: IFileWithHistory
	{
		public string Path {
			get ;
			private set;
		}
		
		public IHistoryProvider Provider { get; private set; }
		
		private Repository repository;
		
		public FileStatus Status {
			get {
				var status = repository.Index.Status;
				var pathToFile = Extensions.ReducePath (repository.WorkingDirectory, Path);
				if (status.Untracked.Contains (pathToFile)) {
					return FileStatus.NotUnderVersionControl;
				} else if (status.Modified.Contains (pathToFile)) {
					return FileStatus.Changed;
				} 
				
				var isClean = AmIClean (status);
				
				if (isClean) {
					return FileStatus.Commited;
				}
				
				return FileStatus.Unknown;
			}
		}

		private bool AmIClean (RepositoryStatus status)
		{
			var filesWithChanges = new List<string> ();
			
			filesWithChanges.AddRange (status.Added);
			filesWithChanges.AddRange (status.Modified);
			filesWithChanges.AddRange (status.Staged);
			
			return ! filesWithChanges.Contains (Path);
		}

		
		public FileWithHistory (IHistoryProvider provider, string path)
		{
			Provider = provider;
			Path = path;
			
			repository = ((HistoryProvider)provider).Repository;
		}
		#region IFileWithHistory implementation
		public string PathInRepository {
			get {
				throw new System.NotImplementedException();
			}
		}
		
		#endregion
	}
}
