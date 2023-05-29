using UnityEditor;

namespace Dragoncraft.Tests
{
    public class BaseTest
    {
        protected UnitData LoadUnit(string unit)
        {
            return AssetDatabase.LoadAssetAtPath<UnitData>($"Assets/Chapter12/Data/Unit/{unit}.asset");
        }

        protected EnemyData LoadEnemy(string enemy)
        {
            return AssetDatabase.LoadAssetAtPath<EnemyData>($"Assets/Chapter12/Data/Enemy/{enemy}.asset");
        }

        protected EnemyGroupData LoadEnemyGroup(string enemyGroup)
        {
            return AssetDatabase.LoadAssetAtPath<EnemyGroupData>($"Assets/Chapter12/Data/EnemyGroup/{enemyGroup}.asset");
        }
    }
}
