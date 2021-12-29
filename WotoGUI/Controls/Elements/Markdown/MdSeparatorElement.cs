// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using WotoGUI.Controls.Overlays.Providers;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdSeparatorElement : MarkdownSeparator
    {
        protected override Drawable CreateSeparator() => 
            new Separator();

        private class Separator : Box
        {
            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                RelativeSizeAxes = Axes.X;
                Height = 1;
                Colour = colourProvider.Background3;
            }
        }
    }
}


