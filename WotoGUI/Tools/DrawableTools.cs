using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Threading;
using osuTK;

namespace WotoGUI.Tools
{
	public static class DrawableTools
	{
		/// <summary>
        /// Shakes this drawable.
        /// </summary>
        /// <param name="target">The target to shake.</param>
        /// <param name="shakeDuration">The length of a single shake.</param>
        /// <param name="shakeMagnitude">Pixels of displacement per shake.</param>
        /// <param name="maximumLength">The maximum length the shake should last.</param>
        public static void Shake(this Drawable target, double shakeDuration = 80, float shakeMagnitude = 8, double? maximumLength = null)
        {
            // if we don't have enough time, don't bother shaking.
            if (maximumLength < shakeDuration * 2)
                return;

            var sequence = target.MoveToX(shakeMagnitude, shakeDuration / 2, Easing.OutSine).Then()
                                 .MoveToX(-shakeMagnitude, shakeDuration, Easing.InOutSine).Then();

            // if we don't have enough time for the second shake, skip it.
            if (!maximumLength.HasValue || maximumLength >= shakeDuration * 4)
            {
                sequence = sequence
                           .MoveToX(shakeMagnitude, shakeDuration, Easing.InOutSine).Then()
                           .MoveToX(-shakeMagnitude, shakeDuration, Easing.InOutSine).Then();
            }

            sequence.MoveToX(0, shakeDuration / 2, Easing.InSine);
        }
	}
}