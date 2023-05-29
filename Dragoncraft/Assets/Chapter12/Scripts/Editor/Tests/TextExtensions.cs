using UnityEngine;

namespace Dragoncraft.Tests
{
    public static class TestExtensions
    {
        public static float GetAttack(this UnitData data, int level)
        {
            return Mathf.Pow(level, data.LevelMultiplier) + data.Attack;
        }

        public static float GetDefense(this UnitData data, int level)
        {
            return Mathf.Pow(level, data.LevelMultiplier) + data.Defense;
        }
    }
}
