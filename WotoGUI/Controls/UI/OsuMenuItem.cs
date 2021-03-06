// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.
#if _NOT_YET_IMPLEMENTED_

using System;
using osu.Framework.Graphics.UserInterface;

namespace WotoGUI.Controls.UI
{
    public class OsuMenuItem : MenuItem
    {
        public readonly MenuItemType Type;

        public OsuMenuItem(string text, MenuItemType type = MenuItemType.Standard)
            : this(text, type, null)
        {
        }

        public OsuMenuItem(string text, MenuItemType type, Action action)
            : base(text, action)
        {
            Type = type;
        }
    }
}

#endif
