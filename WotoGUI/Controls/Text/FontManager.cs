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
using WotoGUI.Client;
using WotoGUI.IO.Store;
using WotoGUI.Controls;

namespace WotoGUI.Controls.Text
{
	public sealed partial class FontManager : IRes, IDisposable
	{
		//-------------------------------------------------
		#region Constant's Region
		/// <summary>
		/// Old Story Bold File In Res.
		/// </summary>
		public const string OSBFileInRes
			= "Old Story Bold.tff";
		/// <summary>
		/// Old Story Bold Italic File In Res.
		/// </summary>
		public const string OSBIFileInRes
			= "Old Story Bold Italic.tff";
		/// <summary>
		/// GUISharpTT Bold File In Res.
		/// </summary>
		public const string GUISharpTTBoldFileInRes
			= "GUISharpWelcomeTT-Bold.tff";
		/// <summary>
		/// GUISharpTT Regular File In Res.
		/// </summary>
		public const string GUISharpTTRFileInRes
			= "GUISharpWelcomeTT-Regular.tff";
		/// <summary>
		/// Noto Sans Regular File In Res.
		/// </summary>
		public const string NSRFileInRes
			= "NotoSansJP-Regular.tff";
		#endregion
		//-------------------------------------------------
		#region static Properties Region

		#endregion
		//-------------------------------------------------
		#region Properties Region
		public WotoRes MyRes
		{
			get => MyStore != null ? MyStore.MainRes : null;
			set {}
		}
		public WotoResourceStore MyStore { get; private set; }
		#endregion
		//-------------------------------------------------
		#region field's Region
		private ClientBinding _binding;
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		private FontManager(ClientBinding binding)
		{
			_binding = binding;
			InitializeComponents();
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		~FontManager()
		{
			Dispose();
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		/// <summary>
		///	Generate a new FontManager.
		/// </summary>
		internal static FontManager GenerateManager(ClientBinding client)
		{
			// check if client is null or not.
			// if there is no client, we SHOULD NOT create a new font manager.
			if (client == null)
			{
				// it means the client is not generated yet, so we should 
				// return null.
				return null;
			}
			// check if the client alread has another font manager or not.
			if (client.FontManager != null)
			{
				// it means a font manager has already been created.
				// so return it instead of creating a new one.
				return client.FontManager;
			}
			// create a new font manager object.
			// please do NOT set the properties and fields in here.
			return new FontManager(client);
		}
		#endregion
		//-------------------------------------------------
	}
}
