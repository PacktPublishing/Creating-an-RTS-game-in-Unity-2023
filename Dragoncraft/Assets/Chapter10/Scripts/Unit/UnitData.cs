using UnityEngine;

namespace Dragoncraft
{
    [CreateAssetMenu(menuName = "Dragoncraft/New Unit")]
    public class UnitData : BaseCharacterData
    {
        public UnitType Type;
        public int Level;
        public float LevelMultiplier;
        public ActionType Actions;
    }
}