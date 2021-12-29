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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
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
using osu.Framework.Platform.SDL2;
using osu.Framework.Platform.Linux;
using osuTK;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Extensions;
using osu.Framework.Testing;
using WotoGUI.IO.Store;
using WotoGUI.Controls;

namespace SibylClient.Configuration
{
	[ExcludeFromDynamicCompile]
	public sealed class WpConfigManager : IniConfigManager<WotoSettings>, IRes
	{
		//-------------------------------------------------
		#region Constant's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public WotoRes MyRes { get; set; }
		public Storage MyStorage { get; private set; }
		public JConfig EmbeddedConfig { get; private set; }
		#endregion
		//-------------------------------------------------
		#region static field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region field's Region
		private bool _initialized;
		#endregion
		//-------------------------------------------------
		#region static event field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region event field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		public WpConfigManager(Storage s) : base(s)
		{
			MyStorage = s;
			InitializeComponent();
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region Initialize Method's Region
		private void InitializeComponent()
		{
			if (_initialized)
			{
				return;
			}
			_initialized = true;
			//---------------------------------------------
			MyRes = new(this.GetType());
			EmbeddedConfig = JConfig.Parse(MyRes.GetString("Config"));
			//---------------------------------------------
		}
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
		protected override void InitialiseDefaults()
		{
			InitializeComponent();
			SetDefault(WotoSettings.WpHostAddress, GetWpAddress());
			SetDefault(WotoSettings.WpHostPort, GetWpPort());
			SetDefault(WotoSettings.WpConnectionType, GetWpConnectionType());
			SetDefault(WotoSettings.SocialvoidEndpoint, GetSvAddress());
		}
		public override TrackedSettings CreateTrackedSettings()
        {
            return base.CreateTrackedSettings();
        }
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public SvHostInfo GetSvInfo() =>
			EmbeddedConfig.SvHostInfo;
		public string GetSvAddress() =>
			EmbeddedConfig.SvHostInfo.Address;
		public WpHostInfo GetWpInfo() =>
			EmbeddedConfig.WpHostInfo;
		public string GetWpAddress() =>
			EmbeddedConfig.WpHostInfo.HostAddress;
		public int GetWpPort() =>
			EmbeddedConfig.WpHostInfo.Port;
		public string GetWpConnectionType() =>
			EmbeddedConfig.WpHostInfo.ConnectionType;
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
	}
}