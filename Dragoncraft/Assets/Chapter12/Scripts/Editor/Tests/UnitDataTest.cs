using NUnit.Framework;
using System.Collections.Generic;

namespace Dragoncraft.Tests
{
    public class UnitDataTest : BaseTest
    {
        private static List<string> units = new List<string>()
        {
            "BasicWarrior",
            "BasicMage"
        };

        [Test]
        public void TestWithUnits([ValueSource("units")] string unit)
        {
            UnitData unitData = LoadUnit(unit);

            Assert.IsNotNull(unitData, $"UnitData {unit} not found.");

            Assert.IsTrue(unitData.Health > 0, "Health must be greater than 0.");
            Assert.IsTrue(unitData.Attack > 0, "Attack must be greater than 0.");
            Assert.IsTrue(unitData.Defense >= 0, "Defense must be equal or greater than 0.");
            Assert.IsTrue(unitData.WalkSpeed > 0, "WalkSpeed must be greater than 0.");
            Assert.IsNotNull(unitData.SelectedColor, "SelectedColor must not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateAttack01), "AnimationStateAttack01 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateAttack02), "AnimationStateAttack02 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateDefense), "AnimationStateDefense must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateMove), "AnimationStateMove must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateIdle), "AnimationStateIdle must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateCollect), "AnimationStateCollect must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(unitData.AnimationStateDeath), "AnimationStateDeath must not be null or empty.");
            Assert.IsTrue(unitData.AttackRange >= 0, "AttackRange must be equal or greater than 0.");
            Assert.IsTrue(unitData.ColliderSize > 0, "ColliderSize must be greater than 0.");

            Assert.IsTrue(unitData.Level >= 0, "Level must be equal or greater than 0.");
            Assert.IsTrue(unitData.LevelMultiplier > 0, "LevelMultiplier must be greater than 0.");
            Assert.IsFalse(unitData.Actions == ActionType.None, "Actions must be different than None.");
        }
    }
}
