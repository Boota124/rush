// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Rush.Tests
{
    [TestFixture]
    public class TestSceneRushPlayer : PlayerTestScene
    {
        public TestSceneRushPlayer()
            : base(new RushRuleset())
        {
        }
    }
}
