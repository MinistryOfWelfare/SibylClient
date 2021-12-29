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


using System.IO;
using WotoGUI.IO.Archives;
using WotoGUI.Utils;
using SharpCompress.Common;

namespace WotoGUI.IO
{
    /// <summary>
    /// An encapsulated import task to be imported to an 
	/// <!-- <see cref="ArchiveModelManager{TModel,TFileModel}"/>. -->
    /// </summary>
    public class ImportTask
    {
        /// <summary>
        /// The path to the file (or filename in the case a stream is provided).
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// An optional stream which provides the file content.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Construct a new import task from a path (on a local filesystem).
        /// </summary>
        public ImportTask(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Construct a new import task from a stream.
        /// </summary>
        public ImportTask(Stream stream, string filename)
        {
            Path = filename;
            Stream = stream;
        }

        /// <summary>
        /// Retrieve an archive reader from this task.
        /// </summary>
        public ArchiveReader GetReader()
        {
            if (Stream != null)
                return new ZipArchiveReader(Stream, Path);

            return getReaderFrom(Path);
        }

        /// <summary>
        /// Creates an <see cref="ArchiveReader"/> from a valid storage path.
        /// </summary>
        /// <param name="path">A file or folder path resolving the archive content.</param>
        /// <returns>A reader giving access to the archive's content.</returns>
        private ArchiveReader getReaderFrom(string path)
        {
            if (ZipUtils.IsZipArchive(path))
                return new ZipArchiveReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read), System.IO.Path.GetFileName(path));
            if (Directory.Exists(path))
                return new LegacyDirectoryArchiveReader(path);
            if (File.Exists(path))
                return new LegacyFileArchiveReader(path);

            throw new InvalidFormatException($"{path} is not a valid archive");
        }

        public override string ToString() => System.IO.Path.GetFileName(Path);
    }
}
