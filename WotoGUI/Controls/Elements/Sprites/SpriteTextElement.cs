// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using WotoGUI.Controls.Text;

namespace WotoGUI.Controls.Elements.Sprites
{
    public class SpriteTextElement : SpriteText
    {
        public SpriteTextElement()
        {
            Shadow = true;
            Font = WotoFont.Default;
        }
    }
}
