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
using System.Drawing;
using WotoGUI.Client;
using WotoGUI.IO.Store;
using WotoGUI.Controls;
using osuTK;


namespace WotoGUI.Controls.Text
{
	public interface ITextMeasurable
	{
		//-------------------------------------------------
		#region Properties Region
		float ElementWidth { get; }
		float ElementHeight { get; }
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		Vector2 MeasureTextSizeVec(string text);
		string GetFixedText(string text);
		#endregion
		//-------------------------------------------------
	}
}
