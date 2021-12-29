// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using Markdig.Extensions.Tables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using WotoGUI.Controls.Text;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdTableCellElement : MarkdownTableCell
    {
        private readonly bool isHeading;

        public MdTableCellElement(TableCell cell, TableColumnDefinition definition, bool isHeading)
            : base(cell, definition)
        {
            this.isHeading = isHeading;
            Masking = false;
            BorderThickness = 0;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(CreateBorder(isHeading));
        }

        public override MarkdownTextFlowContainer CreateTextFlow() => new TableCellTextFlowContainer
        {
            Weight = isHeading ? FontWeight.Bold : FontWeight.Regular,
            Padding = new MarginPadding(10),
        };

        protected virtual Box CreateBorder(bool isHeading)
        {
            if (isHeading)
                return new TableHeadBorder();

            return new TableBodyBorder();
        }

        private class TableHeadBorder : Box
        {
            [BackgroundDependencyLoader]
            //private void load(OverlayColourProvider colourProvider)
            private void load()
            {
                //Colour = colourProvider.Background3;
                Colour = Colour4.Blue;
                RelativeSizeAxes = Axes.X;
                Height = 2;
                Anchor = Anchor.BottomLeft;
                Origin = Anchor.BottomLeft;
            }
        }

        private class TableBodyBorder : Box
        {
            [BackgroundDependencyLoader]
            //private void load(OverlayColourProvider colourProvider)
            private void load()
            {
                //Colour = colourProvider.Background4;
                Colour = Colour4.Red;
                RelativeSizeAxes = Axes.X;
                Height = 1;
            }
        }

        private class TableCellTextFlowContainer : MdTextFlowContainerElement
        {
            public FontWeight Weight { get; set; }

            protected override SpriteText CreateSpriteText() => base.CreateSpriteText().With(t => t.Font = t.Font.With(weight: Weight));
        }
    }
}

