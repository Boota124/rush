// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Rush.Judgements
{
    public class HeartJudgement : RushJudgement
    {
        public override bool AffectsCombo => false;
        public override bool IsBonus => true;

        public override double HealthPoints => 50;
    }
}
