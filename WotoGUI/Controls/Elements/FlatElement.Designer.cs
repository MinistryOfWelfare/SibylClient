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
using System.Reflection;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Versioning;
using osu.Framework;
using osu.Framework.Platform;
using osu.Framework.Graphics.Colour;
using osu.Framework.Configuration;
using osu.Framework.Development;
using osu.Framework.Logging;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.IO.Stores;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osu.Framework.Extensions;
using osu.Framework.Threading;
using System.Net.Http;
using System.Net;
using osu.Framework.Audio.Track;
using osu.Framework.Layout;
using WotoGUI.Controls.Elements;
using WotoGUI.IO.Store;
using WotoGUI.Tools;
using osuTK;


namespace WotoGUI.Controls.Elements
{
	partial class FlatElement
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
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public virtual Vector2 MeasureTextSizeVec(string text) =>
			this.Font.MeasureTextSizeVec(text, this.Spacing);
		public virtual string GetFixedText(string text) => 
			this.FixText(text);
		protected internal virtual void FixMyText()
		{
			if (string.IsNullOrWhiteSpace(this.PrimaryText))
			{
				return;
			}
			this.Text = GetFixedText(this.PrimaryText);
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		public virtual void ChangeText(string text)
		{
			this.PrimaryText = text;
			this.FixMyText();
			this.TextSize = MeasureTextSizeVec(this.Text.ToString());
		}
		public virtual void ChangeAlignment(ContentAlignment alignment)
		{
			this.TextAlign = alignment;
		}
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		#endregion
		//-------------------------------------------------
	}
}