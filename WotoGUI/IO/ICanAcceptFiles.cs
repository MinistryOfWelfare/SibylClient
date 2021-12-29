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

// All credits of this file go to ppy Pty Ltd.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WotoGUI.IO
{
	/// <summary>
    /// A class which can accept files for importing.
    /// </summary>
    public interface ICanAcceptFiles
    {
        /// <summary>
        /// Import the specified paths.
        /// </summary>
        /// <param name="paths">The files which should be imported.</param>
        Task Import(params string[] paths);

        /// <summary>
        /// Import the specified files from the given import tasks.
        /// </summary>
        /// <param name="tasks">The import tasks from which the files should be imported.</param>
        Task Import(params ImportTask[] tasks);

        /// <summary>
        /// An array of accepted file extensions (in the standard format of ".abc").
        /// </summary>
        IEnumerable<string> HandledExtensions { get; }
    }
}