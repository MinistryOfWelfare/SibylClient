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
using System.IO;
using SharpCompress.Archives.Zip;

namespace WotoGUI.Utils
{
    public static class ZipUtils
    {
        public static bool IsZipArchive(string path)
        {
            if (!File.Exists(path))
                return false;

            try
            {
                using (var arc = ZipArchive.Open(path))
                {
                    foreach (var entry in arc.Entries)
                    {
                        using (entry.OpenEntryStream())
                        {
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
