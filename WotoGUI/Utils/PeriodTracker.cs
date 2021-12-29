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
using System.Collections.Generic;
using System.Linq;

namespace WotoGUI.Utils
{
    /// <summary>
    /// Represents a tracking component used for whether a specific time instant falls into any of the provided periods.
    /// </summary>
    public class PeriodTracker
    {
        private readonly List<Period> periods;
        private int nearestIndex;

        public PeriodTracker(IEnumerable<Period> periods)
        {
            this.periods = periods.OrderBy(period => period.Start).ToList();
        }

        /// <summary>
        /// Whether the provided time is in any of the added periods.
        /// </summary>
        /// <param name="time">The time value to check.</param>
        public bool IsInAny(double time)
        {
            if (periods.Count == 0)
                return false;

            if (time > periods[nearestIndex].End)
            {
                while (time > periods[nearestIndex].End && nearestIndex < periods.Count - 1)
                    nearestIndex++;
            }
            else
            {
                while (time < periods[nearestIndex].Start && nearestIndex > 0)
                    nearestIndex--;
            }

            var nearest = periods[nearestIndex];
            return time >= nearest.Start && time <= nearest.End;
        }
    }

    public readonly struct Period
    {
        /// <summary>
        /// The start time of this period.
        /// </summary>
        public readonly double Start;

        /// <summary>
        /// The end time of this period.
        /// </summary>
        public readonly double End;

        public Period(double start, double end)
        {
            if (start >= end)
                throw new ArgumentException($"Invalid period provided, {nameof(start)} must be less than {nameof(end)}");

            Start = start;
            End = end;
        }
    }
}
