// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.
#if _NOT_YET_IMPLEMENTED_

namespace WotoGUI.Controls.UI
{
    public class OsuNumberBox : OsuTextBox
    {
        protected override bool CanAddCharacter(char character) => char.IsNumber(character);
    }
}

#endif
