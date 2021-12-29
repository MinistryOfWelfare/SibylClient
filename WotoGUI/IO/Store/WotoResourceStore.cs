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
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;
using osu.Framework.Graphics.Textures;
using System.Threading;
using SixImage = SixLabors.ImageSharp.Image;
using DImage = System.Drawing.Image;
using osu.Framework.IO.Stores;

namespace WotoGUI.IO.Store
{
	public sealed class WotoResourceStore : ResourceStore<byte[]>
	{
		//-------------------------------------------------
		#region Constants Region
		public const string WotoResStringName = "WotoResourceStore from: ";
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public WotoRes MainRes { get; private set; }
		#endregion
		//-------------------------------------------------
		#region Costructor Region
		public WotoResourceStore(Type t)
		{
			MainRes = GetWotoRes(t);
			AddStore(MainRes);
		}
		public WotoResourceStore(object obj) : this(typeof(object))
		{
			
		}
		#endregion
		//-------------------------------------------------
		#region Ordinary Methods Region
		public void AddToList(string name) => MainRes?.AddToList(name);
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public bool StringExists(string name) =>
			MainRes.StringExists(name);
		public bool ObjectExists(string name) =>
			MainRes.ObjectExists(name);
		public Texture GetAsTexture(string name) =>
			MainRes.GetAsTexture(name);
		public SixImage GetAsSixImage(string name) =>
			MainRes.GetAsSixImage(name);
		public DImage GetAsSystemImage(string name) =>
			MainRes.GetAsSystemImage(name);
		public MemoryStream AllocateMemoryStream(string nameStr) =>
			MainRes.AllocateMemoryStream(nameStr);
		public MemoryStream AllocateMemoryStream(byte[] b) => new(b);
		public byte[] GetBytes(string nameStr) =>
			MainRes.GetBytes(nameStr);
		public override byte[] Get(string name) => GetBytes(name);
		public override Task<byte[]> GetAsync(string name, CancellationToken cancellationToken = default) =>
			Task.FromResult(GetBytes(name));
		public new Stream GetStream(string name) => 
			MainRes.GetStream(name);
		#endregion
		//-------------------------------------------------
		#region Overrided Methods Region
		public string GetString(string strName) => 
			MainRes.GetString(strName);
		public object GetObject(string name) =>
			MainRes.GetObject(name);
		public void ApplyResources(object value, 
			string objectName, 
			CultureInfo culture) =>
			ApplyResources(value, objectName, culture);
		public override string ToString() =>
			WotoResStringName + MainRes.BaseName;
		#endregion
		//-------------------------------------------------
		#region static Methods Region
		private static WotoRes GetWotoRes(Type t) =>
			new(t);
		#endregion
		//-------------------------------------------------
	}
}
