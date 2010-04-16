//  
//  CommitDialog.cs
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

namespace DirectoryHistory.UI
{
	public partial class CommitDialog : Gtk.Dialog
	{
		public CommitDialog ()
		{
			this.Build ();
			
			Modal = true;
			
			buttonOk.Sensitive = false;
			
			textview.Buffer.Changed += HandleTextviewBufferChanged;
		}
		
		
		public string Comment 
		{
			get {
				return textview.Buffer.Text;
			}
		}
		
		private void HandleTextviewBufferChanged (object sender, EventArgs e)
		{
			var stripped = textview.Buffer.Text.Trim ();
			
			if (string.IsNullOrEmpty (stripped)) {
				buttonOk.Sensitive = false;
			}
			else {
				buttonOk.Sensitive = true;
			}
		}
		
	}
}



