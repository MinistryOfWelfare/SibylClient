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
using osu.Framework.Platform.SDL2;
using osu.Framework.Platform.Linux;
using WHost = osu.Framework.Host;

namespace WotoGUI.Client
{
	public partial class AppClient : ClientBinding
	{
		//-------------------------------------------------
		#region Constant's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		public static DesktopGameHost DHost => 
			HasApp ? ActiveClient._dhost : null;
		public static bool HasApp => ActiveClient != null;
		protected internal static AppClient ActiveClient { get; private set; }
		public static Version AssemblyVersion => 
			Assembly.GetEntryAssembly()?.GetName().Version ?? 
			new Version();
		
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public virtual ClientStartMode StartMode { get; protected set; }
		public virtual bool IsDeployedBuild => 
			AssemblyVersion.Major > 0;
		public virtual string ClientVersion
		{
			get
			{
				if (!IsDeployedBuild)
				{
					return @"local " + 
						(DebugUtils.IsDebugBuild ? @"debug" : @"release");
				}

				var version = AssemblyVersion;
				return $@"{version.Major}.{version.Minor}.{version.Build}";
			}
		}
		public virtual string WindowTitle =>
			Window != null ? Window.Title : string.Empty;
		public virtual bool IsPrimaryInstance => 
			_dhost != null ? _dhost.IsPrimaryInstance : false;
		public virtual bool IsDebugBuild =>
			DebugUtils.IsDebugBuild;
		public virtual bool IsNUnitRunning =>
			DebugUtils.IsNUnitRunning;
		public virtual bool LogPerformanceIssues =>
			DebugUtils.LogPerformanceIssues;
		
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
		protected AppClient(DesktopGameHost host, string appName)
		{
			Logger.Level = LogLevel.Error;
			_dhost = host;
			ActiveClient = this;
			AppName = appName;
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		// some members here
		#endregion
		//-------------------------------------------------
	}
}