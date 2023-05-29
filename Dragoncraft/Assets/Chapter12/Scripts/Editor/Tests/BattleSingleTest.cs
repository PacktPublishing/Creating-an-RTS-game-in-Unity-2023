using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using UnityEngine;

namespace Dragoncraft.Tests
{
    public class BattleSingleTest : BaseTest
    {
        private static List<(string, string, int, bool)> battles = new List<(string, string, int, bool)>
        {
            ("BasicWarrior", "BasicOrc",   0, false),
            ("BasicMage",    "BasicOrc",   0, false),
            ("BasicWarrior", "BasicGolem", 0, false),
            ("BasicMage",    "BasicGolem", 0, false),
            ("BasicWarrior", "RedDragon",  0, false),
            ("BasicMage",    "RedDragon",  0, false),
            ("BasicWarrior", "BasicOrc",   1, true),
            ("BasicMage",    "BasicOrc",   1, true),
            ("BasicWarrior", "BasicGolem", 1, false),
            ("BasicMage",    "BasicGolem", 1, false),
            ("BasicWarrior", "RedDragon",  1, false),
            ("BasicMage",    "RedDragon",  1, false),
            ("BasicWarrior", "BasicOrc",   2, true),
            ("BasicMage",    "BasicOrc",   2, true),
            ("BasicWarrior", "BasicGolem", 2, false),
            ("BasicMage",    "BasicGolem", 2, false),
            ("BasicWarrior", "RedDragon",  2, false),
            ("BasicMage",    "RedDragon",  2, false),
            ("BasicWarrior", "BasicOrc",   3, true),
            ("BasicMage",    "BasicOrc",   3, true),
            ("BasicWarrior", "BasicGolem", 3, false),
            ("BasicMage",    "BasicGolem", 3, false),
            ("BasicWarrior", "RedDragon",  3, false),
            ("BasicMage",    "RedDragon",  3, false),
            ("BasicWarrior", "BasicOrc",   4, true),
            ("BasicMage",    "BasicOrc",   4, true),
            ("BasicWarrior", "BasicGolem", 4, false),
            ("BasicMage",    "BasicGolem", 4, false),
            ("BasicWarrior", "RedDragon",  4, false),
            ("BasicMage",    "RedDragon",  4, false),
            ("BasicWarrior", "BasicOrc",   5, true),
            ("BasicMage",    "BasicOrc",   5, true),
            ("BasicWarrior", "BasicGolem", 5, true),
            ("BasicMage",    "BasicGolem", 5, true),
            ("BasicWarrior", "RedDragon",  5, false),
            ("BasicMage",    "RedDragon",  5, false),
        };

        [UnityTest]
        public IEnumerator TestWithSingleEnemy([ValueSource("battles")] (string unit, string enemy, int level, bool unitWin) battle)
        {
            UnitData unitData = LoadUnit(battle.unit);
            float unitHealth = unitData.Health;

            EnemyData enemyData = LoadEnemy(battle.enemy);
            float enemyHealth = enemyData.Health;

            float timer = 0;
            while (unitHealth > 0 && enemyHealth > 0)
            {
                if (timer % unitData.AttackSpeed == 0)
                {
                    enemyHealth -= Mathf.Max(unitData.GetAttack(battle.level) - enemyData.Defense, 0);
                }

                if (timer % enemyData.AttackSpeed == 0 && enemyHealth > 0)
                {
                    unitHealth -= Mathf.Max(enemyData.Attack - unitData.GetDefense(battle.level), 0);
                }

                timer += 0.5f;
                yield return null;
            }

            Assert.AreEqual(battle.unitWin, unitHealth > 0, "The unit was defeated but should have won.");
        }
    }
}
