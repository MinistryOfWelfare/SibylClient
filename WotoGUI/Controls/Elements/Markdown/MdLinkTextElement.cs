// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.



using System.Collections.Generic;
using Markdig.Syntax.Inlines;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;
using WotoGUI.Client;
using WotoGUI.Controls.Text;
using WotoGUI.Controls.Overlays.Providers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osuTK;
using osuTK.Graphics;
//using osu.Game.Online.Chat;
using WotoGUI.Controls.Chat;
//using osu.Game.Overlays;



namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdLinkTextElement : MarkdownLinkText
    {
        [Resolved(canBeNull: true)]
        private AppClient client { get; set; }

        private readonly string text;
        private readonly string title;

        public MdLinkTextElement(string text, LinkInline linkInline)
            : base(text, linkInline)
        {
            this.text = text;
            title = linkInline.Title;
        }

        public MdLinkTextElement(AutolinkInline autolinkInline)
            : base(autolinkInline)
        {
            text = autolinkInline.Url;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var textDrawable = CreateSpriteText().With(t => t.Text = text);

            InternalChildren = new Drawable[]
            {
                textDrawable,
                new MdLinkCompiler(new[] { textDrawable })
                {
                    RelativeSizeAxes = Axes.Both,
                    Action = OnLinkPressed,
                    TooltipText = title ?? Url,
                }
            };
        }

        protected override void OnLinkPressed() => client?.HandleLink(Url);

        private class MdLinkCompiler : DrawableLinkCompiler, IHasTooltip
        {
            public MdLinkCompiler(IEnumerable<Drawable> parts)
                : base(parts)
            {
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                IdleColour = colourProvider.Light2;
                HoverColour = colourProvider.Light1;
            }
        }
    }
}


