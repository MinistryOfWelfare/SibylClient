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

using osu.Framework.Audio;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace WotoGUI.IO
{
	public interface IStorageResourceProvider
	{
		/// <summary>
		/// Retrieve the game-wide audio manager.
		/// </summary>
		AudioManager AudioManager { get; }

		/// <summary>
		/// Access game-wide user files.
		/// </summary>
		IResourceStore<byte[]> Files { get; }

		/// <summary>
		/// Access game-wide resources.
		/// </summary>
		IResourceStore<byte[]> Resources { get; }

		/// <summary>
		/// Create a texture loader store based on an underlying data store.
		/// </summary>
		/// <param name="underlyingStore">The underlying provider of texture data (in arbitrary image formats).</param>
		/// <returns>A texture loader store.</returns>
		IResourceStore<TextureUpload> CreateTextureLoaderStore(IResourceStore<byte[]> underlyingStore);
	}
}
