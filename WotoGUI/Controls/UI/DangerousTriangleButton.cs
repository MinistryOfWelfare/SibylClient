// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#if _NOT_YET_IMPLEMENTED_


using osu.Framework.Allocation;

namespace WotoGUI.Controls.UI
{
    public class DangerousTriangleButton : TriangleButton
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            BackgroundColour = colours.PinkDark;
            Triangles.ColourDark = colours.PinkDarker;
            Triangles.ColourLight = colours.Pink;
        }
    }
}

#endif
