// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.
#if _NOT_YET_IMPLEMENTED_

namespace WotoGUI.Controls.UI
{
    /// <summary>
    /// A <see cref="SearchTextBox"/> which does not handle left/right arrow keys for seeking.
    /// </summary>
    public class SeekLimitedSearchTextBox : SearchTextBox
    {
        public override bool HandleLeftRightArrows => false;
    }
}

#endif
