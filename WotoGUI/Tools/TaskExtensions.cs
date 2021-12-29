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
using osu.Framework.Extensions.ObjectExtensions;

namespace WotoGUI.Tools
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Add a continuation to be performed only after the attached task has completed.
        /// </summary>
        /// <param name="task">The previous task to be awaited on.</param>
        /// <param name="action">The action to run.</param>
        /// <param name="cancellationToken">An optional cancellation token. Will only cancel the provided action, not the sequence.</param>
        /// <returns>A task representing the provided action.</returns>
        public static Task ContinueWithSequential(this Task task, Action action, CancellationToken cancellationToken = default) =>
            task.ContinueWithSequential(() => Task.Run(action, cancellationToken), cancellationToken);

        /// <summary>
        /// Add a continuation to be performed only after the attached task has completed.
        /// </summary>
        /// <param name="task">The previous task to be awaited on.</param>
        /// <param name="continuationFunction">The continuation to run. Generally should be an async function.</param>
        /// <param name="cancellationToken">An optional cancellation token. Will only cancel the provided action, not the sequence.</param>
        /// <returns>A task representing the provided action.</returns>
        public static Task ContinueWithSequential(this Task task, Func<Task> continuationFunction, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<bool>();

            task.ContinueWith(t =>
            {
                // the previous task has finished execution or been cancelled, so we can run the provided continuation.

                if (cancellationToken.IsCancellationRequested)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    continuationFunction().ContinueWith(continuationTask =>
                    {
                        if (cancellationToken.IsCancellationRequested || continuationTask.IsCanceled)
                        {
                            tcs.TrySetCanceled();
                        }
                        else if (continuationTask.IsFaulted)
                        {
                            tcs.TrySetException(continuationTask.Exception.AsNonNull());
                        }
                        else
                        {
                            tcs.TrySetResult(true);
                        }
                    }, cancellationToken: default);
                }
            }, cancellationToken: default);

            // importantly, we are not returning the continuation itself but rather a task which represents its status in sequential execution order.
            // this will not be cancelled or completed until the previous task has also.
            return tcs.Task;
        }
    }
}
