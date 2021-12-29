// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using Markdig.Extensions.Tables;
using osu.Framework.Graphics.Containers.Markdown;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdTableElement : MarkdownTable
    {
        public MdTableElement(Table table)
            : base(table)
        {
        }

        protected override MarkdownTableCell CreateTableCell(TableCell cell, 
            TableColumnDefinition definition, bool isHeading) => 
            new MdTableCellElement(cell, definition, isHeading);
    }
}

