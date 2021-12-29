// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Markdig.Syntax.Inlines;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdImageElement : MarkdownImage, IHasTooltip
    {
        public LocalisableString TooltipText { get; }

        public MdImageElement(LinkInline linkInline)
            : base(linkInline.Url)
        {
            TooltipText = linkInline.Title;
        }
    }
}
