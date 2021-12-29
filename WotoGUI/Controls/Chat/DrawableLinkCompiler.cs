// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using WotoGUI.Controls.Overlays.Providers;
using WotoGUI.Controls.Elements;

namespace WotoGUI.Controls.Chat
{
    /// <summary>
    /// An invisible drawable that brings multiple <see cref="Drawable"/> pieces together to form a consumable clickable link.
    /// </summary>
    public class DrawableLinkCompiler : HoverContainerElement
    {
        /// <summary>
        /// Each word part of a chat link (split for word-wrap support).
        /// </summary>
        public readonly List<Drawable> Parts;

        [Resolved(CanBeNull = true)]
        private OverlayColourProvider overlayColourProvider { get; set; }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => Parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));

        //protected override HoverSounds CreateHoverSounds(HoverSampleSet sampleSet) => 
        //    new LinkHoverSounds(sampleSet, Parts);

        public DrawableLinkCompiler(IEnumerable<Drawable> parts)
            //: base(HoverSampleSet.Submit)
        {
            Parts = parts.ToList();
        }



        [BackgroundDependencyLoader]
        private void load(WotoColor colours)
        {
            IdleColour = overlayColourProvider?.Light2 ?? colours.Blue;
        }

        protected override IEnumerable<Drawable> EffectTargets => Parts;

#if _NOT_YET_IMPLEMENTED_

        private class LinkHoverSounds : HoverClickSounds
        {
            private readonly List<Drawable> parts;

            public LinkHoverSounds(HoverSampleSet sampleSet, List<Drawable> parts)
                : base(sampleSet)
            {
                this.parts = parts;
            }

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => 
                parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));
        }
#endif
    }
}
