using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TestTools;
using UnityEngine;

namespace Dragoncraft.Tests
{
    public class BattleMultiTest : BaseTest
    {
        private static List<(string, List<string>, int, bool)> battles = new List<(string, List<string>, int, bool)>
        {
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicOrc"}, 1, false),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicOrc"}, 2, true),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicOrc", "BasicOrc"}, 2, false),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicGolem"}, 2, false),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicOrc", "BasicOrc"}, 3, false),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicGolem"}, 3, false),
            ("BasicWarrior", new List<string>{"BasicOrc", "BasicOrc", "BasicGolem"}, 3, false)
        };

        [UnityTest]
        public IEnumerator TestWithMultiEnemy([ValueSource("battles")] (string unit, List<string> enemies, int level, bool unitWin) battle)
        {
            UnitData unitData = LoadUnit(battle.unit);
            float unitHealth = unitData.Health;

            List<EnemyData> enemyDataList = new List<EnemyData>();
            List<float> enemyHealthList = new List<float>();
            foreach (string enemy in battle.enemies)
            {
                EnemyData enemyData = LoadEnemy(enemy);
                enemyDataList.Add(enemyData);
                enemyHealthList.Add(enemyData.Health);
            }

            float timer = 0;
            bool battleEnded = false;
            while (unitHealth > 0 && !battleEnded)
            {
                if (timer % unitData.AttackSpeed == 0)
                {
                    for (int i = 0; i < enemyDataList.Count; i++)
                    {
                        if (enemyHealthList[i] > 0)
                        {
                            enemyHealthList[i] -= Mathf.Max(unitData.GetAttack(battle.level) - enemyDataList[i].Defense, 0);
                            break;
                        }
                    }
                }

                for (int i = 0; i < enemyDataList.Count; i++)
                {
                    if (enemyHealthList[i] <= 0)
                    {
                        continue;
                    }

                    if (timer % enemyDataList[i].AttackSpeed == 0)
                    {
                        unitHealth -= Mathf.Max(enemyDataList[i].Attack - unitData.GetDefense(battle.level), 0);
                    }
                }

                battleEnded = !enemyHealthList.Any(i => i > 0);

                timer += 0.5f;
                yield return null;
            }

            Assert.AreEqual(battle.unitWin, unitHealth > 0, "The unit was defeated but should have won.");
        }
    }
}
