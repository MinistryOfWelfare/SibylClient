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

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>.

using System;
using System.Globalization;
using Humanizer;

namespace WotoGUI.Utils
{
    public static class HumanizerUtils
    {
        /// <summary>
        /// Turns the current or provided date into a human readable sentence
        /// </summary>
        /// <param name="input">The date to be humanized</param>
        /// <returns>distance of time in words</returns>
        public static string Humanize(DateTimeOffset input)
        {
            // this works around https://github.com/xamarin/xamarin-android/issues/2012 and https://github.com/Humanizr/Humanizer/issues/690#issuecomment-368536282
            try
            {
                return input.Humanize();
            }
            catch (ArgumentException)
            {
                return input.Humanize(culture: new CultureInfo("en-US"));
            }
        }
    }
}
