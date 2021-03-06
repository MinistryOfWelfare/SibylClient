// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace WotoGUI.Controls.UI
{
    public enum HoverSampleSet
    {
        [Description("default")]
        Default,

        [Description("submit")]
        Submit,

        [Description("button")]
        Button,

        [Description("toolbar")]
        Toolbar,

        [Description("tabselect")]
        TabSelect,

        [Description("scrolltotop")]
        ScrollToTop
    }
}
