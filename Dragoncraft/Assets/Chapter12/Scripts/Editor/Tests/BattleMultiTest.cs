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

        private List<EnemyData> _enemyDataList = new List<EnemyData>();
        private List<float> _enemyHealthList = new List<float>();

        [UnityTest]
        public IEnumerator TestWithMultiEnemy([ValueSource("battles")] (string unit, List<string> enemies, int level, bool unitWin) battle)
        {
            UnitData unitData = LoadUnit(battle.unit);
            float unitHealth = unitData.Health;
            InitializeListsWithEnemies(battle.enemies);

            float timer = 0;
            bool battleEnded = false;
            while (unitHealth > 0 && !battleEnded)
            {
                AttackEnemy(timer, unitData, battle.level);

                AttackUnit(timer, unitData, battle.level, ref unitHealth);

                battleEnded = !_enemyHealthList.Any(i => i > 0);

                timer += 0.5f;
                yield return null;
            }

            Assert.AreEqual(battle.unitWin, unitHealth > 0, "The unit was defeated but should have won.");
        }

        private void InitializeListsWithEnemies(List<string> enemies)
        {
            _enemyDataList.Clear();
            _enemyHealthList.Clear();

            foreach (string enemy in enemies)
            {
                EnemyData enemyData = LoadEnemy(enemy);
                _enemyDataList.Add(enemyData);
                _enemyHealthList.Add(enemyData.Health);
            }
        }

        private void AttackEnemy(float timer, UnitData unitData, int level)
        {
            if (timer % unitData.AttackSpeed == 0)
            {
                for (int i = 0; i < _enemyDataList.Count; i++)
                {
                    if (_enemyHealthList[i] > 0)
                    {
                        _enemyHealthList[i] -= Mathf.Max(unitData.GetAttack(level) - _enemyDataList[i].Defense, 0);
                        break;
                    }
                }
            }
        }

        private void AttackUnit(float timer, UnitData unitData, int level, ref float unitHealth)
        {
            for (int i = 0; i < _enemyDataList.Count; i++)
            {
                if (_enemyHealthList[i] <= 0)
                {
                    continue;
                }

                if (timer % _enemyDataList[i].AttackSpeed == 0)
                {
                    unitHealth -= Mathf.Max(_enemyDataList[i].Attack - unitData.GetDefense(level), 0);
                }
            }
        }
    }
}
