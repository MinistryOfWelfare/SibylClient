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
using System.Collections.Generic;
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
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Graphics.Sprites;
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
using osu.Framework.Layout;
using osuTK;
using osuTK.Input;
using WotoGUI.Controls.Text;
using WotoGUI.Tools;

namespace SibylClient.Screens
{
	partial class ClassicSimpleMusicScreen
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
		protected override bool OnKeyDown(KeyDownEvent e)
		{
			if (!e.HasAnyKeyPressed)
			{
				return false;
			}
			switch (e.Key)
			{
				case Key.Space:
				{
					if (!e.AltPressed && e.ControlPressed)
					{
						this.RunShortcutAction(this.TogglePlayingFromKey);
					}
					return true;
				}
				case Key.Left:
				{
					if (!e.AltPressed && e.ControlPressed)
					{
						this.RunShortcutAction(this.PlayPreviousMusic);
					}
					return true;
				}
				case Key.Right:
				{
					if (!e.AltPressed && e.ControlPressed)
					{
						this.RunShortcutAction(this.PlayNextMusic);
					}
					return true;
				}
			}

			return base.OnKeyDown(e);
		}
		public override void ImportFiles(string[] paths)
		{
			var all = this.GetAllMusics(paths);
			PendingMusics.AddRange(all);
			if (!IsPlayingMusic)
			{
				if (MusicIndex != default)
				{
					MusicIndex = default;
				}
				PlayCurrentPendingMusic();
			}
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
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
		public void TogglePlayingFromKey()
		{
			if (toggling)
			{
				return;
			}
			if (this.PlayingTrack == null || !this.PlayingTrack.IsAlive)
			{
				return;
			}
			toggling = true;
			if (this.PlayingTrack.IsRunning)
			{
				this.StopPlayingTrack();
			}
			else
			{
				this.StartPlayingTrack();
			}
			toggling = false;
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
		private void PlayCurrentPendingMusic()
		{
			PlayingTrack?.Dispose();
			if (!IsPlayingMusic)
			{
				IsPlayingMusic = true;
			}
			var path = PendingMusics[MusicIndex];
			this.PlayingTrack = GetTrackByPath(path);
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
				using (var f = TagLib.File.Create(path))
				{
					this.ChangeMusicArt(f.Tag.ToTextureFromTag());
					this.ChangeMusicName(f.Tag.Title);
				}
			}
		}
		
		[BackgroundDependencyLoader]
		private void load()
		{
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
		public void ChangeMusicName(string name)
		{
			this.TitleSprite.ChangeText(name);
			var c = GetScreenCenter();
			TitleSprite.X = c.X - (TitleSprite.TextSize.X / 4);
			//TitleSprite.Y = (5 * (GetClientSize().Height / 6)) -
			//	(this.TitleSprite.Y / 2);
		}
		public void ChangeMusicArt(Texture texture)
		{
			if (texture == null || texture.Size == default)
			{
				return; //TODO: set something for default maybe?
			}
			this.TrackPictureBox.Texture = texture;
			var csize = GetClientSize();
			var max_width = 4 * (csize.Width / 7);
			var max_height = 4 * (csize.Height / 7);
			Vector2 vec;
			if (texture.Size.X <= max_width && 
				texture.Size.Y >= max_height)
			{
				var height_rate = max_height / texture.Size.Y;
				vec = new(texture.Size.X * height_rate, 
					texture.Size.Y * height_rate);
			}
			else if (texture.Size.X >= max_width)
			{
				var width_rate = max_width / texture.Size.X;
				vec = new(texture.Size.X * width_rate, 
					texture.Size.Y * width_rate);
			}
			else
			{
				vec = texture.Size;
			}
			this.TrackPictureBox.Size = vec;
			this.TrackPictureBox.X = (csize.Width / 2) - 
				(this.TrackPictureBox.Width / 2);
			this.TrackPictureBox.Y = (csize.Height / 2) - 
				(this.TrackPictureBox.Height / 2);
			var bottom = this.TrackPictureBox.Y +
				this.TrackPictureBox.Height;
			if (bottom > this.TitleSprite.ElementTop)
			{
				var diff = (bottom - this.TitleSprite.ElementTop + 4) / 2;
				this.TrackPictureBox.Y -= diff;
				this.TitleSprite.Y += diff;
			}
		}
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		// some methods here
		#endregion
		//-------------------------------------------------
	}
}