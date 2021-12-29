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

namespace WotoGUI.Utils
{
    /// <summary>
    /// A wrapper over a value and a boolean denoting whether the value is valid.
    /// </summary>
    /// <typeparam name="T">The type of value stored.</typeparam>
    public readonly ref struct Optional<T>
    {
        /// <summary>
        /// The stored value.
        /// </summary>
        public readonly T Value;

        /// <summary>
        /// Whether <see cref="Value"/> is valid.
        /// </summary>
        /// <remarks>
        /// If <typeparamref name="T"/> is a reference type, <c>null</c> may be valid for <see cref="Value"/>.
        /// </remarks>
        public readonly bool HasValue;

        private Optional(T value)
        {
            Value = value;
            HasValue = true;
        }

        /// <summary>
        /// Returns <see cref="Value"/> if it's valid, or a given fallback value otherwise.
        /// </summary>
        /// <remarks>
        /// Shortcase for: <c>optional.HasValue ? optional.Value : fallback</c>.
        /// </remarks>
        /// <param name="fallback">The fallback value to return if <see cref="HasValue"/> is <c>false</c>.</param>
        public T GetOr(T fallback) => HasValue ? Value : fallback;

        public static implicit operator Optional<T>(T value) => new Optional<T>(value);
    }
}
