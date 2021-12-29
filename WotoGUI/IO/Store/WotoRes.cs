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
using System.Text;
using System.Globalization;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using osu.Framework.IO.Stores;
using osu.Framework.Graphics.Textures;
using SixImage = SixLabors.ImageSharp.Image;
using DImage = System.Drawing.Image;

namespace WotoGUI.IO.Store
{
	public sealed class WotoRes : ComponentResourceManager, IResourceStore<byte[]>
	{
		//-------------------------------------------------
		#region Constants Region
		public const string WotoResStringName = "WotoRes from: ";
		#endregion
		//-------------------------------------------------
		#region Properties Region
		internal List<string> ResList { get; set; } = new();
		#endregion
		//-------------------------------------------------
		#region Costructor Region
		public WotoRes(Type t) : base(t)
		{
			;
		}
		public WotoRes(object obj) : this(typeof(object))
		{

		}
		#endregion
		//-------------------------------------------------
		#region Ordinary Methods Region
		public void AddToList(string name) => ResList?.Add(name);
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public bool StringExists(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return false;
			}
			try
			{
				return GetObject(name) is string;
			}
			catch
			{
				return false;
			}
			
		}
		public bool ObjectExists(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return false;
			}
			try
			{
				return !(GetObject(name) is null);	
			}
			catch
			{
				return false;
			}
		}
		public Texture GetAsTexture(string name)
		{
			var s = GetStream(name);
			if (s == null || s.Length == 0 || !s.CanRead)
			{
				return null;
			}
			return Texture.FromStream(s);
		}
		public SixImage GetAsSixImage(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			var b = GetBytes(name);
			if (b == null || b.Length == 0)
			{
				return null;
			}
			return SixImage.Load(b);
		}
		public DImage GetAsSystemImage(string name)
		{
			if (string.IsNullOrEmpty(name) || !ObjectExists(name))
			{
				return null;
			}
			var obj = GetObject(name);
			if (obj is byte[] b)
			{
				if (b.Length == 0)
				{
					return null;
				}
				return DImage.FromStream(AllocateMemoryStream(b));
			}
			else if (obj is DImage image)
			{
				return image;
			}
			else if (obj is Stream s)
			{
				// it's unlikely that the object received from
				// our ResourceManager is a stream... 
				// but added it for just-in-case.
				if (s.Length == 0 || !s.CanRead)
				{
					return null;
				}
				return DImage.FromStream(s);
			}
			return null;
		}
		public MemoryStream AllocateMemoryStream(string nameStr)
		{
			if (string.IsNullOrWhiteSpace(nameStr))
			{
				return null;
			}
			var b = GetBytes(nameStr);
			if (b == null || b.Length == default)
			{
				return null;
			}
			return new(b);
		}
		public MemoryStream AllocateMemoryStream(byte[] b) => new(b);
		public byte[] GetBytes(string nameStr)
		{
			var r = base.GetObject(nameStr);
			if (r is byte[] b)
			{
				return b;
			}
			return null;
		}
		public byte[] Get(string name) => GetBytes(name);
		public Task<byte[]> GetAsync(string name) => null;
		public new Stream GetStream(string name)
		{
			try
			{
				return base.GetStream(name);
			}
			catch
			{
				var b = GetBytes(name);
				return new MemoryStream(b);
			}
		}
		public IEnumerable<string> GetAvailableResources() => ResList;
		public void Dispose() => base.ReleaseAllResources();
		#endregion
		//-------------------------------------------------
		#region Overrided Methods Region
		public override string GetString(string strName)
		{
			try
			{
				return base.GetString(strName);
			}
			catch (InvalidOperationException e)
			{
				if (!e.Message.Contains("call GetObject instead")) throw;
				var b = GetBytes(strName);
				if (b == null || b.Length == 0)
				{
					return null;
				}
				return Encoding.UTF8.GetString(b);
			}
		}
		public override object GetObject(string name)
		{
			return base.GetObject(name);
		}
		public override void ApplyResources(object value, string objectName, CultureInfo culture)
		{
			base.ApplyResources(value, objectName, culture);
		}
		public override string ToString()
		{
			return WotoResStringName + BaseName;
		}
		#endregion
		//-------------------------------------------------
	}
}
