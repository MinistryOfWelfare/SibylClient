// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.
#if _NOT_YET_IMPLEMENTED_

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace WotoGUI.Controls.UI
{
    public class SlimEnumDropdown<T> : OsuEnumDropdown<T>
        where T : struct, Enum
    {
        protected override DropdownHeader CreateHeader() => new SlimDropdownHeader();

        private class SlimDropdownHeader : OsuDropdownHeader
        {
            public SlimDropdownHeader()
            {
                Height = 25;
                Foreground.Padding = new MarginPadding { Top = 4, Bottom = 4, Left = 8, Right = 4 };
            }
        }
    }
}

#endif
