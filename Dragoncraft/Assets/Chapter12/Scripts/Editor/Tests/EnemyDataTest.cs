using NUnit.Framework;
using System.Collections.Generic;

namespace Dragoncraft.Tests
{
    public class EnemyDataTest : BaseTest
    {
        private static List<string> enemies = new List<string>()
        {
            "BasicOrc",
            "BasicGolem",
            "RedDragon"
        };

        [Test]
        public void TestWithEnemies([ValueSource("enemies")] string enemy)
        {
            EnemyData enemyData = LoadEnemy(enemy);

            Assert.IsNotNull(enemyData, $"EnemyData {enemy} not found.");

            Assert.IsTrue(enemyData.Health > 0, "Health must be greater than 0.");
            Assert.IsTrue(enemyData.Attack > 0, "Attack must be greater than 0.");
            Assert.IsTrue(enemyData.Defense >= 0, "Defense must be equal or greater than 0.");
            Assert.IsTrue(enemyData.WalkSpeed > 0, "WalkSpeed must be greater than 0.");
            Assert.IsNotNull(enemyData.SelectedColor, "SelectedColor must not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateAttack01), "AnimationStateAttack01 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateAttack02), "AnimationStateAttack02 must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateDefense), "AnimationStateDefense must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateMove), "AnimationStateMove must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateIdle), "AnimationStateIdle must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateCollect), "AnimationStateCollect must not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(enemyData.AnimationStateDeath), "AnimationStateDeath must not be null or empty.");
            Assert.IsTrue(enemyData.AttackRange >= 0, "AttackRange must be equal or greater than 0.");
            Assert.IsTrue(enemyData.ColliderSize > 0, "ColliderSize must be greater than 0.");
        }
    }
}
