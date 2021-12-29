﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if _NOT_YET_IMPLEMENTED_

using System;

namespace WotoGUI.Controls.UI
{
    public class OsuEnumDropdown<T> : OsuDropdown<T>
        where T : struct, Enum
    {
        public OsuEnumDropdown()
        {
            Items = (T[])Enum.GetValues(typeof(T));
        }
    }
}

#endif 
