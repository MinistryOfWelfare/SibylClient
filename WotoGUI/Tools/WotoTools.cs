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
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
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
using osu.Framework.Extensions;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osu.Framework.Platform.SDL2;
using osu.Framework.Platform.Linux;
using osu.Framework.Graphics.Sprites;
using osuTK;
using TagLib;
using WotoGUI.Controls.Text;
using TagFile = TagLib.File;

namespace WotoGUI.Tools
{
	public static class WotoTools
	{
		//-------------------------------------------------
		#region Constant's Region
		// ReSharper disable once InconsistentNaming
		// ReSharper disable once MemberCanBePrivate.Global
		public const char UNSIGNED_CHAR1	= '\b';
		// ReSharper disable once InconsistentNaming
		// ReSharper disable once MemberCanBePrivate.Global
		public const char UNSIGNED_CHAR2	= '\r';
		// ReSharper disable once InconsistentNaming
		// ReSharper disable once MemberCanBePrivate.Global
		public const char SIGNED_CHAR1	  = '\n';
		// ReSharper disable once InconsistentNaming
		// ReSharper disable once MemberCanBePrivate.Global
		public const char SIGNED_CHAR2	  = ' ';
		public const char SIGNED_CHAR3	  = '、';
		public const int DEFAULT_Z_BASE		   = 0b0;
		public const int DEFAULT_A_BASE		   = 0b1;
		public const int DEFAULT_B_BASE		   = 0b10;
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
		#region extension method region
		public static Vector2 ToVector2(this Size s) => 
			new(s.Width, s.Height);
		public static Vector2 MeasureTextSizeVec(this FontUsage font, 
			string text, Vector2 spacing = default)
		{
			int lines = 1;
			int length = text.Length;
			string best = text;
			if (text.Contains("\n"))
			{
				var all = text.Split("\n");

				foreach (var t in all)
				{
					if (t.Length > best.Length)
					{
						best = t;
					}
				}
				lines = all.Length;
			}
			
			return new(((best.Length * font.Size) / 2) + ((best.Length - 1) * spacing.X), 
				(lines * font.Size) + ((lines - 1) * spacing.Y));
		}
		public static Size MeasureTextSize(this FontUsage font, string text) =>
			MeasureTextSizeVec(font, text).ToSize();
		public static Size ToSize(this Vector2 v) => 
			new((int)v.X, (int)v.Y);
		public static string FixText(this ITextMeasurable measurable, 
			string text, float maxWidth = default)
		{
			if (maxWidth == default)
			{
				maxWidth = measurable.ElementWidth;
				if (maxWidth == default)
				{
					return text;
				}
			}
			if (confirmedMe())
			{
				return text;
			}
			List<string> finalList = new();
			string[] myStrings = text.Split(SIGNED_CHAR1.ToString());
			string[] bySpace;
			char[] chars;
			string rString = string.Empty;
			string lastRString = rString;
			foreach (var n in myStrings)
			{
				// var ns = n.GetValue();
				if (confirmed(n))
				{
					finalList.Add(n);
					clear();
					continue;
				}
				bySpace = n.Split(SIGNED_CHAR2.ToString());
				if (bySpace.Length == 1)
				{
					if (confirmed(bySpace[DEFAULT_Z_BASE]))
					{
						finalList.Add(lastRString);
						clear();
					}
					else
					{
						clear();
						chars = bySpace[0].ToCharArray();
						for(int i = 0; i < chars.Length; i++)
						{
							var c = chars[i];
							rString += c;
							if (confirmed(rString))
							{
								lastRString = rString;
								if (i == chars.Length - 1)
								{
									finalList.Add(lastRString);
									clear();
									// ReSharper disable once RedundantJumpStatement
									continue;
								}
							}
							else
							{
								finalList.Add(lastRString);
								clear();
								if (i == chars.Length - 1)
								{
									finalList.Add(c.ToString());
								}
								else
								{
									rString += c;
								}
							}
						}
					}
				}
				else
				{
					bool charWorked = false;
					// by space
					for(int i = 0; i < bySpace.Length; i++)
					{
						var s = bySpace[i];
						if (!confirmed(s))
						{
							charWorker(s);
							charWorked = true;
							continue;
						}
						rString += SIGNED_CHAR2 + s;
						if (confirmed(rString))
						{
							lastRString = rString;
							if (i == bySpace.Length - 1)
							{
								if (charWorked)
								{
									var m = finalList[^1] + SIGNED_CHAR2 + lastRString;
									if (confirmed(m))
									{
										finalList[^DEFAULT_A_BASE] = m;
									}
									else
									{
										finalList.Add(lastRString);
									}
									clear();
									charWorked = false;
								}
								else
								{
									finalList.Add(lastRString);
									clear();
								}
							}
						}
						else
						{
							if (charWorked)
							{
								var m = finalList[^1] + SIGNED_CHAR2 + lastRString;
								if (confirmed(m))
								{
									finalList[^DEFAULT_A_BASE] = m;
								}
								else
								{
									finalList.Add(lastRString);
								}
								clear();
								charWorked = false;
							}
							else
							{
								finalList.Add(lastRString);
								clear();
							}
							if (i == bySpace.Length - 1)
							{
								finalList.Add(s);
							}
							else if (hasSpace(s))
							{
								if (i == bySpace.Length - 2)
								{
									finalList.Add(s);
								}
							}
							else
							{
								rString += SIGNED_CHAR2 + s;
							}
						}
					}
				}
			}
			return buildMe();

			//local functions:
			string buildMe()
			{
				var myString = string.Empty;
				var f = string.Empty;
				for (int i = 0; i < finalList.Count; i++)
				{
					f = finalList[i];
					if (!string.IsNullOrEmpty(f))
					{
						if (i == finalList.Count - 1)
						{
							if (hasSpaceMe())
							{
								myString += f;
								break;
							}
						}
						myString += SIGNED_CHAR1 + f;
					}
				}
				if (hasSpaceMe())
				{
					if (!hasSpace(myString))
					{
						myString += SIGNED_CHAR2;
					}
				}
				return myString;
			}
			bool confirmed(string s)
			{
				return measurable.MeasureTextSizeVec(s).X < maxWidth;
			}
			bool confirmedMe()
			{
				return confirmed(text);
			}
			void clear()
			{
				lastRString = rString = string.Empty;
			}
			bool hasSpace(string s)
			{
				return s[^1] == SIGNED_CHAR2;
			}
			bool hasSpaceMe()
			{
				return text[^DEFAULT_A_BASE] == SIGNED_CHAR2;
			}
			void charWorker(string rs)
			{
				if (confirmed(rs))
				{
					finalList.Add(rs);
					return;
				}
				var frString = string.Empty;
				var flastRString = frString;
				var fake_chars = rs.ToCharArray();
				for (int i = 0; i < fake_chars.Length; i++)
				{
					var fc = fake_chars[i];
					frString += fc;
					if (confirmed(frString))
					{
						flastRString = frString;
						if (i == fake_chars.Length - 1)
						{
							finalList.Add(flastRString);
							clearFakes();
							// continue to another turn of loop
							// ReSharper disable once RedundantJumpStatement
							continue;
						}
					}
					else
					{
						finalList.Add(flastRString);
						clearFakes();
						if (i == fake_chars.Length - 1)
						{
							finalList.Add(fc.ToString());
						}
						else
						{
							frString += fc;
						}
					}
				}
				return;
				void clearFakes()
				{
					flastRString = frString = string.Empty;
				}
			}
		}
		public static Texture ToTextureFromTagFile(this TagFile t) =>
			t == null ? null : t.Tag.ToTextureFromTag();
		public static Texture GetTexture(this ResourceStore<byte[]> store, string name) =>
			Texture.FromStream(store.GetStream(name));
		public static Texture ToTextureFromTag(this Tag t)
		{
			if (t == null || t.Pictures == null || t.Pictures.Length == 0)
			{
				return null;
			}
			return t.Pictures[0x0].ToTexture();
		}
		public static Texture ToTexture(this IPicture p)
		{
			if (p == null)
			{
				return null;
			}
			Texture texture = null;
			try
			{
				using (var m = new MemoryStream(p.Data.Data))
				{
					texture = Texture.FromStream(m);
				}
			}
			catch
			{
				return texture;
			}
			return texture;
		}
		#endregion
		//-------------------------------------------------
		#region static method region
		public static Texture GetTextureFromFile(string path)
		{
			Texture texture = null;
			using (var f = TagFile.Create(path))
			{
				texture = f.ToTextureFromTagFile();
			}
			return texture;
		}
		#endregion
		//-------------------------------------------------
	}
}

