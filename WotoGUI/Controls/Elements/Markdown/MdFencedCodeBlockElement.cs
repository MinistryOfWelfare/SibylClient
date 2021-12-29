// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.



using Markdig.Syntax;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using WotoGUI.Controls.Overlays.Providers;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdFencedCodeBlockElement : MarkdownFencedCodeBlock
    {
        // TODO : change to monospace font for this component
        public MdFencedCodeBlockElement(FencedCodeBlock fencedCodeBlock)
            : base(fencedCodeBlock)
        {
        }

        protected override Drawable CreateBackground() => 
            new CodeBlockBackground();

        public override MarkdownTextFlowContainer CreateTextFlow() => 
            new CodeBlockTextFlowContainer();

        private class CodeBlockBackground : Box
        {
            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            //private void load()
            {
                RelativeSizeAxes = Axes.Both;
                //Colour = colourProvider.Background6;
                Colour = Colour4.Red;
            }
        }

        private class CodeBlockTextFlowContainer : MdTextFlowContainerElement
        {
            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            //private void load()
            {
                Colour = colourProvider.Light1;
                Colour = Colour4.Red;
                Margin = new MarginPadding(10);
            }
        }
    }
}



