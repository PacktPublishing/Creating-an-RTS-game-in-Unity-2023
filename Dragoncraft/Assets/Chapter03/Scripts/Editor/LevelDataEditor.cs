using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dragoncraft
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LevelData levelData = (LevelData)target;

            AddLevelDetails(levelData);

            AddLevelSlots(levelData);

            AddButtonInitialize(levelData);

            AddButtonUpdate(levelData);
        }

        private void AddLevelDetails(LevelData levelData)
        {
            levelData.Configuration = EditorGUILayout.ObjectField("Level: ",
                levelData.Configuration, typeof(LevelConfiguration), false) as LevelConfiguration;

            levelData.Columns = EditorGUILayout.IntSlider("Columns: ",
                levelData.Columns, 1, 25);

            levelData.Rows = EditorGUILayout.IntSlider("Rows: ",
                levelData.Rows, 1, 25);
        }

        private void AddLevelSlots(LevelData levelData)
        {
            EditorGUILayout.LabelField("Level Item per position:");

            for (int x = 0; x < levelData.Rows; x++)
            {
                GUILayout.BeginHorizontal();

                for (int y = 0; y < levelData.Columns; y++)
                {
                    LevelSlot slot = FindLevelSlot(levelData.Slots, x, y);
                    slot.ItemType = (LevelItemType)EditorGUILayout.EnumPopup(slot.ItemType);
                }

                GUILayout.EndHorizontal();
            }
        }

        private LevelSlot FindLevelSlot(List<LevelSlot> slots, int x, int y)
        {
            LevelSlot slot = slots.Find(i => i.Coordinates.x == x &&
                                             i.Coordinates.y == y);
            if (slot == null)
            {
                slot = new LevelSlot(LevelItemType.None, new Vector2Int(x, y));
                slots.Add(slot);
            }

            return slot;
        }

        private void AddButtonInitialize(LevelData levelData)
        {
            if (GUILayout.Button("Initialize"))
            {
                Initialize(levelData);
            }
        }

        private void AddButtonUpdate(LevelData levelData)
        {
            if (GUILayout.Button("Update"))
            {
                EditorUtility.SetDirty(levelData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void Initialize(LevelData levelData)
        {
            levelData.Slots.Clear();

            for (int x = 0; x < levelData.Rows; x++)
            {
                for (int y = 0; y < levelData.Columns; y++)
                {
                    LevelSlot levelSlot = new LevelSlot(LevelItemType.None, new Vector2Int(x, y));
                    levelData.Slots.Add(levelSlot);
                }
            }
        }
    }
}