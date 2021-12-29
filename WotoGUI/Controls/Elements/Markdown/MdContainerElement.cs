// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Markdig.Extensions.Tables;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Sprites;
//using osu.Game.Graphics.Sprites;
using WotoGUI.Controls.Elements.Sprites;
using WotoGUI.Controls.Text;

namespace WotoGUI.Controls.Elements.Markdown
{
    public class MdContainerElement : MarkdownContainer
    {
        public MdContainerElement()
        {
            LineSpacing = 21;
        }

        protected override void AddMarkdownComponent(IMarkdownObject markdownObject, FillFlowContainer container, int level)
        {
            switch (markdownObject)
            {
                case YamlFrontMatterBlock _:
                    // Don't parse YAML Frontmatter
                    break;

                case ListItemBlock listItemBlock:
                    bool isOrdered = ((ListBlock)listItemBlock.Parent)?.IsOrdered == true;

                    MdListItemElement childContainer = CreateListItem(listItemBlock, level, isOrdered);

                    container.Add(childContainer);

                    foreach (var single in listItemBlock)
                        base.AddMarkdownComponent(single, childContainer.Content, level);
                    break;

                default:
                    base.AddMarkdownComponent(markdownObject, container, level);
                    break;
            }
        }

        public override SpriteText CreateSpriteText() => new SpriteTextElement
        {
            Font = WotoFont.GetFont(Typeface.Inter, size: 14, weight: FontWeight.Regular),
        };

        public override MarkdownTextFlowContainer CreateTextFlow() => 
            new MdTextFlowContainerElement();

        protected override MarkdownHeading CreateHeading(HeadingBlock headingBlock) => 
            new MdHeadingElement(headingBlock);

        protected override MarkdownFencedCodeBlock CreateFencedCodeBlock(FencedCodeBlock fencedCodeBlock) => 
            new MdFencedCodeBlockElement(fencedCodeBlock);

        protected override MarkdownSeparator CreateSeparator(ThematicBreakBlock thematicBlock) =>
            new MdSeparatorElement();

        protected override MarkdownQuoteBlock CreateQuoteBlock(QuoteBlock quoteBlock) => 
            new MdQuoteBlockElement(quoteBlock);

        protected override MarkdownTable CreateTable(Table table) => 
            new MdTableElement(table);

        protected override MarkdownList CreateList(ListBlock listBlock) => new MarkdownList
        {
            Padding = new MarginPadding(0)
        };

        protected virtual MdListItemElement CreateListItem(ListItemBlock listItemBlock, int level, bool isOrdered)
        {
            if (isOrdered)
                return new MdOrderedListItemElement(listItemBlock.Order);

            return new MdUnorderedListItemElement(level);
        }

        protected override MarkdownPipeline CreateBuilder()
            => new MarkdownPipelineBuilder().UseAutoIdentifiers(AutoIdentifierOptions.GitHub)
                                            .UseEmojiAndSmiley()
                                            .UseYamlFrontMatter()
                                            .UseAdvancedExtensions().Build();
    }
}


