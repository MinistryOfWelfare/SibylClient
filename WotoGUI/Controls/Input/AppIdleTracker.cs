// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.
#if _NOT_YET_IMPLEMENTED_

using osu.Framework.Input;

namespace WotoGUI.Controls.Input
{
    public class AppIdleTracker : IdleTracker
    {
        private InputManager inputManager;

        public AppIdleTracker(int time)
            : base(time)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            inputManager = GetContainingInputManager();
        }

        protected override bool AllowIdle => 
            inputManager.FocusedDrawable == null;
    }
}

#endif
