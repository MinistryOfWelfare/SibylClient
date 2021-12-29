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
using System.Net;
using System.Net.Http;
using System.Text.Encodings;
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
using osu.Framework.IO.Network;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osu.Framework.Utils;

namespace WotoGUI.IO.Store
{
	public class StreamingStorage: IResourceStore<byte[]>
	{
		//-------------------------------------------------
		#region Constant's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		public static StreamingStorage AbsoluteFileStore { get; } = new();
		#endregion
		//-------------------------------------------------
		#region Properties Region
		
		#endregion
		//-------------------------------------------------
		#region static field's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region field's Region
		protected HttpClient _httpClient;
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
		public StreamingStorage()
		{
			
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		// some members here
		#endregion
		//-------------------------------------------------
		#region Get Method's region
		public async Task<byte[]> GetAsync(string url,
			CancellationToken cancellationToken = default)
		{
			_httpClient ??= new HttpClient();
			return await _httpClient.GetByteArrayAsync(url, cancellationToken);
		}

		public virtual byte[] Get(string url)
		{
			var client = new HttpClient();
			var t = client.GetByteArrayAsync(url);
			t.Wait();
			return t.Result;
		}

		public Stream GetStream(string url)
		{
			var client = new HttpClient();
			var s =  client.GetStreamAsync(url);
			s.Wait();
			return s.Result;
		}

		public IEnumerable<string> GetAvailableResources() => 
			Enumerable.Empty<string>();

		#endregion
		//-------------------------------------------------
		#region IDisposable Support
		public void Dispose()
		{
		}
		#endregion
		//-------------------------------------------------
	}
}
