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

#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using WotoGUI.Tools;

namespace WotoGUI.Utils
{
    /// <summary>
    /// A chain of <see cref="Task"/>s that run sequentially.
    /// </summary>
    public class TaskChain
    {
        private readonly object taskLock = new object();

        private Task lastTaskInChain = Task.CompletedTask;

        /// <summary>
        /// Adds a new task to the end of this <see cref="TaskChain"/>.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for this task. Does not affect further tasks in the chain.</param>
        /// <returns>The awaitable <see cref="Task"/>.</returns>
        public Task Add(Action action, CancellationToken cancellationToken = default)
        {
            lock (taskLock)
                return lastTaskInChain = lastTaskInChain.ContinueWithSequential(action, cancellationToken);
        }

        /// <summary>
        /// Adds a new task to the end of this <see cref="TaskChain"/>.
        /// </summary>
        /// <param name="task">The task to be executed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for this task. Does not affect further tasks in the chain.</param>
        /// <returns>The awaitable <see cref="Task"/>.</returns>
        public Task Add(Func<Task> task, CancellationToken cancellationToken = default)
        {
            lock (taskLock)
                return lastTaskInChain = lastTaskInChain.ContinueWithSequential(task, cancellationToken);
        }
    }
}
