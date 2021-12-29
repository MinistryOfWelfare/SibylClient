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
using osu.Framework.Graphics.Animations;
using osu.Framework.Graphics.UserInterface;
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
using osu.Framework.Audio.Track;
using osu.Framework.Layout;
using WotoGUI.Controls.Elements;
using WotoGUI.IO.Store;
using WotoGUI.Client;
using WotoGUI.Screens;
using SibylClient.Screens;
using SibylClient.Configuration;



namespace SibylClient.Client
{
	public partial class WotoClient
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
		public override void SetHost(GameHost host)
		{
			base.SetHost(host);
			this.LocalConfig = new(host.Storage);
		}
		protected override void LoadComplete()
		{
			base.LoadComplete();
			//try
			//{
				//var t = GetTrackByPath("/home/mrwoto/Music/1. LiSA, Uru - Saikai (produced by Ayase).mp3");
				//t.Completed += () =>
				//{
				//	var another = GetTrackByPath("/home/mrwoto/Music/01 - Kyoumen no Nami.mp3");
				//	Audio.AddItem(another);
				//	another.Start();
				//};
				//Audio.AddItem(t);
				//t.Start();
			//}
			//catch (Exception e)
			//{
			//	Console.Write(e);
			//}
			//ManagedBass.Bass.PluginLoad()
			this.InitializeBackground();
			ScreenBase screen = null;
			//this.StartMode = ClientStartMode.ClassicSimpleMusic;
			switch (this.StartMode)
			{
				case ClientStartMode.Normal:
					if (IsFirstTime())
					{
						if (GlobalStorage.Exists(Constants.WpDataDir))
						{
							GlobalStorage.Delete(Constants.WpDataDir);
						}
						screen = new MainScreen(this);
					}
					//screen = new ClassicSimpleMusicScreen();
					//screen = new LoginScreen();
					break;
				default:
					return;
			}
			this.ScreenStack.Push(screen);
			
			//var s = new StreamingStorage();
			//var t = this.Audio.GetTrackStore(s).Get("https://ice.ayokaacr.de/stream.mp3");
			//this.Audio.AddItem(t);
		}
		protected override void ImportFiles(string[] path)
		{
			if (this.ScreenStack == null)
			{
				// TODO: handle especial cases where we don't have any
				// screen.
				return; 
			}
			switch (this.ScreenStack.CurrentScreen)
			{
				case ClassicSimpleMusicScreen csms:
					csms.ImportFiles(path);
					break;
				default:
					return;
			}
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		[BackgroundDependencyLoader]
		private void Load()
		{
			this.Child = ScreenStack = new() 
			{
				RelativeSizeAxes = Axes.Both,
			};
		}
		
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		private bool IsFirstTime() => 
			!GlobalStorage.ExistsDirectory(Constants.WpDataDir);
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		public static AppClient GetWotoClient(AppHostProvider hostProvider)
		{
			if (ActiveClient != null)
			{
				return ActiveClient;
			}

			return new WotoClient(hostProvider.Host, hostProvider.Name);
		}
		#endregion
		//-------------------------------------------------
	}
}