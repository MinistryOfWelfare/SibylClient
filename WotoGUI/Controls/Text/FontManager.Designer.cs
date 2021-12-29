/*
 * This file is part of WotoGUI Project (https://github.com/RudoRonuma/SibylClient).
 * Copyright (c) 2021 WotoGUI Authors.
 *
 * This library is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this source code of library. 
 * If not, see <http://www.gnu.org/licenses/>.
 */

using System; 

namespace WotoGUI.Controls.Text
{
	partial class FontManager
	{
		//-------------------------------------------------
		#region Initialize Method's Region
		/// <summary>
		/// Initializ the components.
		/// </summary>
		private void InitializeComponents()
		{
			//---------------------------------------------
			//news:
			this.MyStore = new(typeof(FontManager));
			//---------------------------------------------
			this.MyStore.AddExtension("ttf");
			this.MyStore.AddToList(NSRFileInRes);
			this.AddAllFonts();
			// localFunctions:
			//---------------------------------------------
		}
		#endregion
		//-------------------------------------------------
		#region Ordinary Method's Region
		public void Dispose()
		{
			if (this.MyRes != null)
			{
				this.MyRes = null;
			}
			#if (OLD_GUISharp)
			if (this._collection != null)
			{
				this._collection = null;
			}
			#endif
		}
		private void AddAllFonts()
		{
			try
			{

				this._binding.AddFont(this.MyStore, NSRFileInRes);
				this._binding.AddFont(this.MyStore, OSBFileInRes);
				this._binding.AddFont(this.MyStore, OSBIFileInRes);
				this._binding.AddFont(this.MyStore, GUISharpTTBoldFileInRes);
				this._binding.AddFont(this.MyStore, GUISharpTTRFileInRes);
			}
			catch (Exception ex)
			{
				Console.Write(ex);
				return;
			}
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		#endregion
		//-------------------------------------------------
	}
}