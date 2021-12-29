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
using System.Text.Encodings;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using osuTK;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Extensions;
using osu.Framework.Testing;
using WotoGUI.IO.Store;
using WotoGUI.IO;
using WotoGUI.Controls;

namespace SibylClient.IPC
{
	public class ArchiveImportIPCChannel : IpcChannel<ArchiveImportMessage>
	{
		private readonly ICanAcceptFiles importer;

		public ArchiveImportIPCChannel(IIpcHost host, ICanAcceptFiles importer = null)
			: base(host)
		{
			this.importer = importer;
			MessageReceived += msg =>
			{
				Debug.Assert(importer != null);
				ImportAsync(msg.Path).ContinueWith(t =>
				{
					if (t.Exception != null) throw t.Exception;
				}, TaskContinuationOptions.OnlyOnFaulted);
			};
		}

		public async Task ImportAsync(string path)
		{
			if (importer == null)
			{
				// we want to contact a remote osu! to handle the import.
				await SendMessageAsync(new ArchiveImportMessage { Path = path }).ConfigureAwait(false);
				return;
			}

			if (importer.HandledExtensions.Contains(Path.GetExtension(path)?.ToLowerInvariant()))
				await importer.Import(path).ConfigureAwait(false);
		}
	}

	public class ArchiveImportMessage
	{
		public string Path;
	}
}