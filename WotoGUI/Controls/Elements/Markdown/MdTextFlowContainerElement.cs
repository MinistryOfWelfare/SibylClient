// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using Markdig.Syntax.Inlines;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using WotoGUI.Controls.Overlays.Providers;
using WotoGUI.Controls.Text;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdTextFlowContainerElement : MarkdownTextFlowContainer
    {
        protected override void AddLinkText(string text, LinkInline linkInline)
            => AddDrawable(new MdLinkTextElement(text, linkInline));

        protected override void AddAutoLink(AutolinkInline autolinkInline)
            => AddDrawable(new MdLinkTextElement(autolinkInline));

        protected override void AddImage(LinkInline linkInline) => 
            AddDrawable(new MdImageElement(linkInline));

        // TODO : Change font to monospace
        protected override void AddCodeInLine(CodeInline codeInline) => 
            AddDrawable(new MdInlineCodeElement
            {
                Text = codeInline.Content
            });

        protected override SpriteText CreateEmphasisedSpriteText(bool bold, bool italic)
            => CreateSpriteText().With(t => t.Font = t.Font.With(weight: bold ? FontWeight.Bold : FontWeight.Regular, italics: italic));

        private class MdInlineCodeElement : Container
        {
            [Resolved]
            private IMarkdownTextComponent parentTextComponent { get; set; }

            public string Text;

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                AutoSizeAxes = Axes.Both;
                CornerRadius = 4;
                Masking = true;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = colourProvider.Background6,
                    },
                    parentTextComponent.CreateSpriteText().With(t =>
                    {
                        t.Colour = colourProvider.Light1;
                        t.Text = Text;
                        t.Padding = new MarginPadding
                        {
                            Vertical = 1,
                            Horizontal = 4,
                        };
                    }),
                };
            }
        }
    }
}

