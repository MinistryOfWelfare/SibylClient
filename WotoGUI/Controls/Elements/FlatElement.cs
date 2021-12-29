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
using osu.Framework.Threading;
using osu.Framework.Text;
using osu.Framework.Platform.SDL2;
using osu.Framework.Platform.Linux;
using osuTK;
using osu.Framework.Graphics.Sprites;
using WotoGUI.Controls.Text;

namespace WotoGUI.Controls.Elements
{
	public partial class FlatElement : SpriteText, ITextMeasurable
	{
		//-------------------------------------------------
		#region Constant's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public virtual Vector2 TextSize { get; private set; }
		public virtual float ElementWidth => Width;
		public virtual float ElementHeight => Height;
		public virtual float ElementBottom => Y + Width;
		public virtual float ElementTop => Y;
		public virtual float ElementLeft => X;
		public virtual float ElementRight => X + Width;
		public virtual Vector2 ElementTopLeft => Position;
		public virtual Vector2 ElementTopRight => 
			new(ElementRight, ElementTop);
		public virtual Vector2 ElementBottomLeft =>
			new(X, ElementBottom);
		public virtual Vector2 ElementBottomRight => 
			new(ElementRight, ElementBottom);
		public virtual string PrimaryText { get; protected set; }
		public virtual ContentAlignment TextAlign { get; protected set; } = 
			ContentAlignment.MiddleCenter;
		#endregion
		//-------------------------------------------------
		#region static field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region field's Region
		
		#endregion
		//-------------------------------------------------
		#region static event field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region event field's Region
		//public virtual 
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		public FlatElement()
		{
			
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		// some members here
		#endregion
		//-------------------------------------------------
	}
}