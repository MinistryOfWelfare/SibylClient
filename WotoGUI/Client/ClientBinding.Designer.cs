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
using osu.Framework.Audio.Track;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
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
using osu.Framework.Extensions;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Layout;
using osu.Framework.IO.Stores;
using osu.Framework.Localisation;
using SibylClient.Resources;
using WotoGUI.Controls.Elements;
using WotoGUI.IO.Store;
using WotoGUI.Tools;
using WotoGUI.Controls.Text;
using WotoGUI.Controls.Chat.Links;


namespace WotoGUI.Client
{
	public partial class ClientBinding : Game
	{
		//-------------------------------------------------
		#region Initialize Method's Region
		private void InitializeComponent()
		{
			
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
		protected override void LoadComplete()
		{
			base.LoadComplete();
		}
		public override void SetHost(GameHost host)
		{
			base.SetHost(host);
			if (host.Window is SDL2DesktopWindow w)
			{
				SDL = w;
				var iconStream = GetIconStream();
				if (iconStream != null)
				{
					SDL.SetIconFromStream(iconStream);
				}
				SDL.Title = AppName;
				SDL.DragDrop += f => fileDrop(new[] { f });
				var p = GetDisplayCenter();
				SDL.Position = GetDisplayCenter();
				host.DrawThread.ActiveHz /= 3;
				host.DrawThread.InactiveHz /= 3;
				//Size = new(200, 200);
				//SDL.WindowState = WindowState.Maximised;
				//SDL.WindowMode.Value = WindowMode.Borderless;
				//var desktopWindow = (SDL2DesktopWindow)host.Window;

				//desktopWindow.CursorState |= CursorState.Hidden;
				//desktopWindow.SetIconFromStream(iconStream);
				//desktopWindow.Title = Name;
				//desktopWindow.DragDrop += f => fileDrop(new[] { f });
			}
			
		}
		protected override void Update()
		{
			base.Update();
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		public virtual void HandleLink(string url)
		{
			
		}
		public virtual void HandleLink(LinkDetails details)
		{
			
		}
		public virtual void ChangeBackgroundColor()
		{

		}
		protected internal virtual void InitializeBackground()
		{
			if (ClientBackground == null)
			{
				ClientBackground = new();
				AddInternal(ClientBackground);
			}
		}
		[BackgroundDependencyLoader]
		private void Load()
		{
			//this.FontManager = FontManager.GenerateManager(this);
			Resources.AddStore(GetDllResourceStore());

			
			AddFont(Resources, @"Fonts/Torus/Torus-Regular");
			AddFont(Resources, @"Fonts/Torus/Torus-Light");
			AddFont(Resources, @"Fonts/Torus/Torus-SemiBold");
			AddFont(Resources, @"Fonts/Torus/Torus-Bold");

			AddFont(Resources, @"Fonts/Inter/Inter-Regular");
			AddFont(Resources, @"Fonts/Inter/Inter-RegularItalic");
			AddFont(Resources, @"Fonts/Inter/Inter-Light");
			AddFont(Resources, @"Fonts/Inter/Inter-LightItalic");
			AddFont(Resources, @"Fonts/Inter/Inter-SemiBold");
			AddFont(Resources, @"Fonts/Inter/Inter-SemiBoldItalic");
			AddFont(Resources, @"Fonts/Inter/Inter-Bold");
			AddFont(Resources, @"Fonts/Inter/Inter-BoldItalic");

			AddFont(Resources, @"Fonts/Noto/Noto-Basic");
			AddFont(Resources, @"Fonts/Noto/Noto-Hangul");
			AddFont(Resources, @"Fonts/Noto/Noto-CJK-Basic");
			AddFont(Resources, @"Fonts/Noto/Noto-CJK-Compatibility");
			AddFont(Resources, @"Fonts/Noto/Noto-Thai");

			AddFont(Resources, @"Fonts/Venera/Venera-Light");
			AddFont(Resources, @"Fonts/Venera/Venera-Bold");
			AddFont(Resources, @"Fonts/Venera/Venera-Black");
		}
		private DllResourceStore GetDllResourceStore() =>
			new(WotoResources.ResourceAssembly);
		private void fileDrop(string[] filePaths)
		{
			lock (importableFiles)
			{
				var firstExtension = Path.GetExtension(filePaths.First());

				if (filePaths.Any(f => Path.GetExtension(f) != firstExtension))
				{
					return;
				}

				importableFiles.AddRange(filePaths);

				Logger.Log($"Adding {filePaths.Length} files for import");
				// File drag drop operations can potentially trigger hundreds
				// or thousands of these calls on desktop platform.
				// In order to avoid spawning multiple import tasks
				// for a single drop operation, debounce a touch.
				importSchedule?.Cancel();
				importSchedule = Scheduler.AddDelayed(handlePendingImports, 100);
			}
		}
		private void handlePendingImports()
		{
			if (importableFiles == null || importableFiles.Count == 0)
			{
				return;
			}
			lock (importableFiles)
			{
				
				Logger.Log($"Handling batch import of {importableFiles.Count} files");

				var paths = importableFiles.ToArray();

				importableFiles.Clear();

				ImportFiles(paths);
			}
		}
		protected virtual void ImportFiles(string[] path)
		{
			;
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public Size GetClientSize() =>
			SDL != null ? SDL.ClientSize : default;
		public Point GetClientCenter()
		{
			var s = GetClientSize();
			if (s == default)
			{
				return default;
			}
			return new(s.Width / 2, s.Height / 2);
		}
		public Rectangle GetDisplayRectangle() =>
			SDL != null ? SDL.CurrentDisplay.Bounds : default;
		public Point GetDisplayCenter()
		{
			var rect = GetDisplayRectangle();
			if (rect == default)
			{
				return default;
			}
			return new((rect.Width / 2) - (SDL.ClientSize.Width / 2),
				(rect.Height / 2) - (SDL.ClientSize.Height / 2));
		}
		/// <summary>
		/// Gets a <see cref="Track"/> value by the specified path.
		/// </summary>
		public virtual Track GetTrackByPath(string path) 
		{
			if (absPathTrackStore == null)
			{
				absPathTrackStore = Audio.GetTrackStore(WFileStore.AbsoluteFileStore);
			}
			var ret = absPathTrackStore.Get(path);
			if (ret == null)
			{
				try
				{
					var res = absPathTrackStore.GetAsync(path);
					res.Wait();
					return res.Result;
				}
				catch
				{
					return null;
				}
			}
			return ret;
		}
		public virtual async Task<Track> GetTrackByPathAsync(string path) 
		{
			if (absPathTrackStore == null)
			{
				absPathTrackStore = Audio.GetTrackStore(WFileStore.AbsoluteFileStore);
			}
			var ret = await absPathTrackStore.GetAsync(path);
			if (ret == null)
			{
				try
				{
					var res = absPathTrackStore.GetAsync(path);
					res.Wait();
					return res.Result;
				}
				catch
				{
					return null;
				}
			}
			return ret;
		}
		public virtual Track GetTrackByUrl(string url)
		{
			if (onlineTrackStore == null)
			{
				onlineTrackStore = Audio.GetTrackStore(onlineStore);
			}
			var ret = onlineTrackStore.Get(url);
			if (ret == null)
			{
				try
				{
					var res = onlineTrackStore.GetAsync(url);
					res.Wait();
					return res.Result;
				}
				catch
				{
					return null;
				}
			}
			return ret;
		}
		public virtual async Task<Track> GetTrackByUrlAsync(string url)
		{
			if (onlineTrackStore == null)
			{
				onlineTrackStore = Audio.GetTrackStore(onlineStore);
			}
			var ret = await onlineTrackStore.GetAsync(url);
			if (ret == null)
			{
				try
				{
					var res = onlineTrackStore.GetAsync(url);
					res.Wait();
					return res.Result;
				}
				catch
				{
					return null;
				}
			}
			return ret;
		}
		public Stream GetIconStream()
		{
			var asm = Assembly.GetEntryAssembly();
			return GetIconByAsm(asm) ?? GetLibIcon();
		}
		public Stream GetLibIcon()
		{
			var s = GetType().Assembly;
			return s == null ? null : GetIconByAsm(s);
		}
		private Stream GetIconByAsm(Assembly asm)
		{
			var n = asm.EntryPoint.DeclaringType.Namespace;
			return 
				asm.GetManifestResourceStream(n + ".Icon.bmp") ??
				asm.GetManifestResourceStream("Icon.bmp") ??
				asm.GetManifestResourceStream(n + ".Icon.ico") ??
				asm.GetManifestResourceStream("Icon.ico");
		}
		public virtual Assembly GetEntryAssembly() =>
			DebugUtils.GetEntryAssembly();
		public virtual string GetEntryPath() =>
			AppContext.BaseDirectory;
		internal virtual DesktopGameHost GetDHost() =>
			_dhost;
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