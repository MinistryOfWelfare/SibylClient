// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdOrderedListItemElement : MdListItemElement
    {
        private const float left_padding = 30;

        private readonly int order;

        public MdOrderedListItemElement(int order)
        {
            this.order = order;
            Padding = new MarginPadding { Left = left_padding };
        }

        protected override SpriteText CreateMarker() => base.CreateMarker().With(t =>
        {
            t.X = -left_padding;
            t.Text = $"{order}.";
        });
    }
}

