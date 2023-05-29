using NUnit.Framework;
using System.Collections.Generic;

namespace Dragoncraft.Tests
{
    public class EnemyGroupDataTest : BaseTest
    {
        private static List<string> groups = new List<string>()
        {
            "OrcDuo",
            "GolemAndOrcs"
        };

        [Test]
        public void TestWithGroups([ValueSource("groups")] string group)
        {
            EnemyGroupData enemyGroupData = LoadEnemyGroup(group);

            Assert.IsNotNull(enemyGroupData, $"EnemyGroupData {group} not found.");

            Assert.IsFalse(string.IsNullOrEmpty(enemyGroupData.Name), "Name must not be null or empty.");
            Assert.IsFalse(enemyGroupData.Enemies.Count == 0, "Enemies list must have at least one EnemyData.");
        }
    }
}
