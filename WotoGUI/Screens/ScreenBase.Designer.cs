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
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework;
using osu.Framework.Platform;
using osu.Framework.Graphics.Colour;
using osu.Framework.Configuration;
using osu.Framework.Development;
using osu.Framework.Logging;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Input;
using osu.Framework.Extensions;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.IO.Stores;
using osu.Framework.Localisation;
using osu.Framework.Layout;
using WotoGUI.Client;
using WotoGUI.Tools;
using WotoGUI.IO.Store;

namespace WotoGUI.Screens
{
	partial class ScreenBase
	{
		//-------------------------------------------------
		#region Initialize Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region Graphical Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region event Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region overrided Method's Region
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		public virtual void AddTrackItem(Track t) =>
			ActiveClient?.Audio.AddItem(t);
		public abstract void ImportFiles(string[] paths);
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		/// <summary>
		/// Gets a <see cref="Track"/> value by the specified path.
		/// </summary>
		public virtual Track GetTrackByPath(string path) =>
			ActiveClient.GetTrackByPath(path);
		public virtual Track GetTrackByRes(string name) =>
			ActiveClient.GetTrackByRes(name);
		public virtual Track GetTrackByUrl(string url) =>
			ActiveClient.GetTrackByUrl(url);
		public virtual async Task<Track> GetTrackByUrlAsync(string url) =>
			await ActiveClient.GetTrackByUrlAsync(url);
		public Point GetScreenCenter() =>
			AppClient.HasApp ?
			AppClient.ActiveClient.GetClientCenter() : default;
		public Size GetClientSize() =>
			AppClient.HasApp ?
			AppClient.ActiveClient.GetClientSize() : default;
		public Rectangle GetDisplayRectangle() =>
			AppClient.HasApp ?
			AppClient.ActiveClient.GetDisplayRectangle() : default;
		public Point GetDisplayCenter() =>
			AppClient.HasApp ? 
			AppClient.ActiveClient.GetDisplayCenter() : default;
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		public virtual void ChangeBackgroundColor(Colour4 colour) =>
			ClientBackground?.ChangeColor(colour);
		public virtual void ChangeBackgroundTexture(Texture texture) =>
			ClientBackground?.ChangeTexture(texture);
		public virtual void ChangeTextureFromRes(string name) =>
			this.ChangeBackgroundTexture(
				this.BindingResources.GetTexture(name)
			);
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
	}
}