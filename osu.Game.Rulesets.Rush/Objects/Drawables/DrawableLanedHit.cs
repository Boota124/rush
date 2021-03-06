// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI.Scrolling;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Rush.Objects.Drawables
{
    public class DrawableLanedHit<TLanedHit> : DrawableRushHitObject<TLanedHit>, IDrawableLanedHit
        where TLanedHit : LanedHit
    {
        public virtual Color4 LaneAccentColour => HitObject.Lane == LanedHitLane.Air ? AIR_ACCENT_COLOUR : GROUND_ACCENT_COLOUR;

        public Anchor LaneAnchor =>
            HitObject.Lane switch
            {
                LanedHitLane.Air => Direction.Value == ScrollingDirection.Left ? Anchor.TopLeft : Anchor.TopRight,
                LanedHitLane.Ground => Direction.Value == ScrollingDirection.Left ? Anchor.BottomLeft : Anchor.BottomRight,
                _ => Direction.Value == ScrollingDirection.Left ? Anchor.BottomLeft : Anchor.BottomRight
            };

        public Anchor LeadingAnchor => Direction.Value == ScrollingDirection.Left ? Anchor.CentreLeft : Anchor.CentreRight;

        public Anchor TrailingAnchor => Direction.Value == ScrollingDirection.Left ? Anchor.CentreRight : Anchor.CentreLeft;

        public LanedHitLane Lane => HitObject.Lane;

        public DrawableLanedHit(TLanedHit hitObject)
            : base(hitObject)
        {
        }

        protected override void OnDirectionChanged(ValueChangedEvent<ScrollingDirection> e)
        {
            base.OnDirectionChanged(e);

            Anchor = LaneAnchor;
        }

        public override bool OnPressed(RushAction action)
        {
            if (!LaneMatchesAction(action))
                return false;

            return UpdateResult(true);
        }

        public virtual bool LaneMatchesAction(RushAction action)
        {
            switch (HitObject.Lane)
            {
                case LanedHitLane.Air:
                    return action == RushAction.AirPrimary || action == RushAction.AirSecondary;

                case LanedHitLane.Ground:
                    return action == RushAction.GroundPrimary || action == RushAction.GroundSecondary;
            }

            return false;
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            Debug.Assert(HitObject.HitWindows != null);

            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = HitResult.Miss);
                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);
            if (result == HitResult.None)
                return;

            ApplyResult(r => r.Type = result);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            AccentColour.Value = LaneAccentColour;
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            base.UpdateStateTransforms(state);

            switch (state)
            {
                case ArmedState.Miss:
                    AccentColour.Value = Color4.Gray;
                    break;
            }
        }
    }
}
