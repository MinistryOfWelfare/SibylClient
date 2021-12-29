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
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
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
using osu.Framework.Screens;
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
using osu.Framework.Layout;
using osu.Framework.Threading;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Sprites;
using WotoGUI.Client;
using WotoGUI.Screens;
using WotoGUI.Controls.Text;
using WotoGUI.Controls.Elements;

namespace SibylClient.Screens
{
	public sealed partial class MainScreen : ScreenBase
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
		public FlatElement TitleSprite { get; set; }
		public Sprite TrackPictureBox { get; set; }
		public Track PlayingTrack { get; set; }
		public List<string> PendingMusics { get; set; } = new();
		public int MusicIndex { get; set; }
		public bool IsPlayingMusic { get; set; }
		#endregion
		//-------------------------------------------------
		#region static field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region field's Region
		private ScheduledDelegate shortcutScheduled;
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
		public MainScreen(ClientBinding binding = null) : 
			base(binding)
		{
			
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		// some members here
		#endregion
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
		
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
	}
}