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
using System.Net;
using osu.Framework.Logging;
using WotoGUI.Client;
using Sentry;

namespace WotoGUI.Utils
{
    /// <summary>
    /// Report errors to sentry.
    /// </summary>
    public class SentryLogger : IDisposable
    {
        private SentryClient sentry;
        private Scope sentryScope;

        public SentryLogger(AppClient game)
        {
            if (!game.IsDeployedBuild) return;

            var options = new SentryOptions
            {
                Dsn = "https://key@sentry.io/num",
                Release = game.Version
            };

            sentry = new SentryClient(options);
            sentryScope = new Scope(options);

            Exception lastException = null;

            Logger.NewEntry += entry =>
            {
                if (entry.Level < LogLevel.Verbose) return;

                var exception = entry.Exception;

                if (exception != null)
                {
                    if (!shouldSubmitException(exception))
                        return;

                    // since we let unhandled exceptions go ignored at times, we want to ensure they don't get submitted on subsequent reports.
                    if (lastException != null &&
                        lastException.Message == exception.Message && exception.StackTrace.StartsWith(lastException.StackTrace, StringComparison.Ordinal))
                        return;

                    lastException = exception;
                    sentry.CaptureEvent(new SentryEvent(exception) { Message = entry.Message }, sentryScope);
                }
                else
                    sentryScope.AddBreadcrumb(DateTimeOffset.Now, entry.Message, entry.Target.ToString(), "navigation");
            };
        }

        private bool shouldSubmitException(Exception exception)
        {
            switch (exception)
            {
                case IOException ioe:
                    // disk full exceptions, see https://stackoverflow.com/a/9294382
                    const int hr_error_handle_disk_full = unchecked((int)0x80070027);
                    const int hr_error_disk_full = unchecked((int)0x80070070);

                    if (ioe.HResult == hr_error_handle_disk_full || ioe.HResult == hr_error_disk_full)
                        return false;

                    break;

                case WebException we:
                    switch (we.Status)
                    {
                        // more statuses may need to be blocked as we come across them.
                        case WebExceptionStatus.Timeout:
                            return false;
                    }

                    break;
            }

            return true;
        }

        #region Disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool isDisposed;

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            isDisposed = true;
            sentry?.Dispose();
            sentry = null;
            sentryScope = null;
        }

        #endregion
    }
}
