using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dragoncraft
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LevelData levelData = (LevelData)target;

            levelData.Configuration = EditorGUILayout.ObjectField("Configuration: ", levelData.Configuration, typeof(LevelConfiguration), true) as LevelConfiguration;

            levelData.Columns = EditorGUILayout.IntSlider("Columns: ", levelData.Columns, 1, 25);

            levelData.Rows = EditorGUILayout.IntSlider("Rows: ", levelData.Rows, 1, 25);

            EditorGUILayout.LabelField("Level Item per position:");

            for (int i = 0; i < levelData.Slots.Count; i++)
            {
                if (i % levelData.Rows == 0)
                {
                    GUILayout.BeginHorizontal();
                }

                levelData.Slots[i].ItemType = (LevelItemType)EditorGUILayout.EnumPopup(levelData.Slots[i].ItemType);

                if ((i + 1) % levelData.Columns == 0)
                {
                    GUILayout.EndHorizontal();
                }
            }

            if (GUILayout.Button("Initialize"))
            {
                Initialize(levelData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

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

            for (int x = 0; x < levelData.Columns; x++)
            {
                for (int y = 0; y < levelData.Rows; y++)
                {
                    levelData.Slots.Add(new LevelSlot
                    {
                        ItemType = LevelItemType.None,
                        Coordinates = new Vector2Int(x, y)
                    });
                }
            }
        }

        private LevelItemType FindLevelItemTypeAt(List<LevelSlot> slots, int x, int y)
        {
            foreach (LevelSlot slot in slots)
            {
                if (slot.Coordinates.x == x && slot.Coordinates.y == y)
                {
                    return slot.ItemType;
                }
            }

            return LevelItemType.None;
        }
    }
}