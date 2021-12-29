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
using osu.Framework.Extensions;
using osu.Framework.Threading;
using System.Net.Http;
using System.Net;
using osu.Framework.Audio.Track;
using osu.Framework.Layout;
using WotoGUI.Controls.Elements;
using WotoGUI.IO.Store;
using WHost = osu.Framework.Host;


namespace WotoGUI.Client
{
	public partial class AppClient
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
		
		[BackgroundDependencyLoader]
		private void Load()
		{
			try
			{
				using (var str = File.OpenRead(GetType().Assembly.Location))
				{
					// please see here:
					// https://github.com/ppy/osu-framework/blob/master/osu.Framework/Extensions/ExtensionMethods.cs
					VersionHash = str.ComputeMD5Hash();
				}
			}
			catch
			{
				// special case for android builds, which can't read DLLs from a packed apk.
				// should eventually be handled in a better way.
				VersionHash = $"{Version}-{RuntimeInfo.OS}".ComputeMD5Hash();
			}
		}
		protected void LoadComplete_sample()
		{
			try
			{
				//var httpClient = new HttpClient();
				//httpClient.GetStreamAsync("https://raw.githubusercontent.com/pokurt/Akagi-Voice-Lines/main/Everlasting%20Catharsis.mp3");
				//HttpRequestMessage req = new("");
				// WebClient c = new();
				// c.DownloadFileAsync(new("https://ice.ayokaacr.de/stream.mp3"), "mystream_test.mp3");
				//Thread.Sleep(2000);
				/*
				Task.Run(() =>
				{
					WebClient c = new WebClient();
					//c.DownloadFile("https://raw.githubusercontent.com/pokurt/Akagi-Voice-Lines/main/Everlasting%20Catharsis.mp3",
					//	"mystream_test.mp3");
					c.DownloadFile("https://raw.githubusercontent.com/ALiwoto/OsuTest1/master/1.%20LiSA%2C%20Uru%20-%20Saikai%20(produced%20by%20Ayase).mp3",
						"mystream_test.mp3");
				});
				Task.Run(() =>
				{
					Thread.Sleep(2000);
					var f = File.OpenRead("mystream_test.mp3");
					//var f = File.OpenRead("/home/mrwoto/Music/1. LiSA, Uru - Saikai (produced by Ayase).mp3");
					TrackBass t = new(f);
					Audio.AddItem(t);
					t.Start();
				});
				*/

				//var t = Audio.GetTrackStore(WFileStore.AbsoluteFileStore).Get("/home/mrwoto/Music/1. LiSA, Uru - Saikai (produced by Ayase).mp3");
				
				var t = GetTrackByPath("/home/mrwoto/Music/1. LiSA, Uru - Saikai (produced by Ayase).mp3");

				//var t = await Audio.GetTrackStore(new OnlineStore()).GetAsync("https://raw.githubusercontent.com/ALiwoto/OsuTest1/master/1.%20LiSA%2C%20Uru%20-%20Saikai%20(produced%20by%20Ayase).mp3");
				
				Audio.AddItem(t);
				t.Start();




				//WebClient c = new WebClient();
				//var f = File.OpenRead("/home/mrwoto/Music/1. LiSA, Uru - Saikai (produced by Ayase).mp3");
				//TrackBass t = new(f);
				//Audio.TrackMixer.Add(t);
				//OnlineStore s = new();
				//t.Volume.Value = 1;
				//t.Start();
				//Audio.AddItem(t);
				//Console.Write("here");
			}
			catch (Exception e)
			{
				Console.Write(e);
			}
		}
		
		

		public virtual void Run(ClientStartMode mode = ClientStartMode.Normal)
		{
			StartMode = mode;
			_dhost?.Run(this);
		}
		public virtual void DisposeHost()
		{
			_dhost?.Dispose();
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		public static AppClient GetAppClient(string appName)
		{
			if (ActiveClient != null)
			{
				return ActiveClient;
			}

			return new(WHost.GetSuitableHost(appName), appName);
		}
		#endregion
		//-------------------------------------------------
	}
}