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
using System.Net.Sockets;
using System.Net;
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
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Text;
using osu.Framework.Graphics.Containers.Markdown;
using Markdig.Extensions.Tables;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.IO.Stores;
using osu.Framework.Localisation;
using osu.Framework.Layout;
using WotoGUI.Client;
using WotoGUI.Tools;
using WotoGUI.IO.Store;

namespace SibylClient.Screens
{
	partial class MainScreen
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
		public void Track_Completed()
		{
			this.PlayNextMusic();
		}
		#endregion
		//-------------------------------------------------
		#region overrided Method's Region
		
		public override void ImportFiles(string[] paths)
		{

		}
		protected override void LoadComplete()
		{
			base.LoadComplete();
			this.ChangeTextureFromRes(@"Backgrounds/bg1.png");
			this.AddNewMusic(@"Musics/05 - PSYCHO-PASS feat.AKANE (Movie Mix).mp3");
			this.AddNewMusic(@"Musics/06 - fiqf (Movie Mix).mp3");
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		public void AddNewMusic(string name) 
		{
			PendingMusics.Add(name);
			if (!IsPlayingMusic)
			{
				if (MusicIndex != default)
				{
					MusicIndex = default;
				}
				this.PlayCurrentPendingMusic();
			}
		}
		private void PlayCurrentPendingMusic()
		{
			PlayingTrack?.Dispose();
			if (!IsPlayingMusic)
			{
				IsPlayingMusic = true;
			}
			var path = PendingMusics[MusicIndex];
			this.PlayingTrack = GetTrackByRes(path);
			this.PlayingTrack.Completed += Track_Completed;
			try
			{
				this.AddTrackItem(PlayingTrack);
				this.StartPlayingTrack();
			}
			catch
			{
				// the file wasn't a music file at all.
				Track_Completed();
				return;
			}
			// ensure that bitrate and length are not zero.
			if (PlayingTrack.Bitrate == 0 && PlayingTrack.Length == 0)
			{
				// it was a good attempt, but it seems like this file format
				// is not supported.
				Track_Completed();
				return;
			}
			else
			{
				//using (var f = TagLib.File.Create(path))
				//{
					//this.ChangeMusicArt(f.Tag.ToTextureFromTag());
					//this.ChangeMusicName(f.Tag.Title);
				//}
			}
		}
		public void RunShortcutAction(Action task)
		{
			if (shortcutScheduled != null)
			{
				return;
			}
			try
			{
				shortcutScheduled = Scheduler.AddDelayed(() => {
					task?.Invoke();
					shortcutScheduled = null;
				}, 200);
			}
			catch (Exception e)
			{
				Logger.Error(e, "when trying to run a shortcut action");
			}
		}
		public void PlayNextMusic()
		{
			if (PendingMusics == null)
			{
				return;
			}
			if (MusicIndex + 1 >= PendingMusics.Count)
			{
				IsPlayingMusic = false;
				//PendingMusics.Clear();
				return;
			}
			MusicIndex++;
			PlayCurrentPendingMusic();
		}
		public void PlayPreviousMusic()
		{
			if (PendingMusics == null || PendingMusics.Count == 0)
			{
				return;
			}
			if (MusicIndex - 1 < 0)
			{
				IsPlayingMusic = false;
				//PendingMusics.Clear();
				return;
			}
			MusicIndex--;
			PlayCurrentPendingMusic();
		}
		public void StopPlayingTrack()
		{
			try
			{
				this.PlayingTrack?.Stop();
			}
			catch (Exception e)
			{
				Logger.Error(e, "tried to stop the track");
			}
		}
		public void StartPlayingTrack()
		{
			try
			{
				this.PlayingTrack?.Start();
			}
			catch (Exception e)
			{
				Logger.Error(e, "tried to start the track");
			}
		}
		[BackgroundDependencyLoader]
		private void load()
		{
			/*
			this.TitleSprite = new();
			this.TrackPictureBox = new();
			//---------------------------------------------
			var c = GetScreenCenter();
			TitleSprite.Size = new(270, 30);
			TitleSprite.X = c.X - (TitleSprite.Width / 2);
			TitleSprite.Y = (5 * (GetClientSize().Height / 6)) -
				(TitleSprite.Height / 2);
				
			TitleSprite.Colour = Colour4.WhiteSmoke;
			Schedule(()=> 
			{
				ClientBackground.Add(TitleSprite);
				ClientBackground.Add(TrackPictureBox);
			});
			//this.AddInternal(TrackPictureBox);
			//this.AddInternal(TitleSprite);
			*/
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public List<string> GetAllMusics(IEnumerable<string> collection)
		{
			if (collection == null)
			{
				return null;
			}
			var musics = new List<string>();
			foreach (var path in collection)
			{
				if (Directory.Exists(path))
				{
					try
					{
						musics.AddRange(Directory.GetFiles(path));
					}
					catch
					{
						continue;
					}
				}
				else if (File.Exists(path))
				{
					musics.Add(path);
				}
			}
			return musics;
		}
		
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
