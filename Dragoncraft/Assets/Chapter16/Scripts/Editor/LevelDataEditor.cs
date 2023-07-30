#if UNITY_EDITOR
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
            // The target is the ScriptableObject, but we need to cast it to the class type we want (LevelData)
            LevelData levelData = (LevelData)target;

            AddLevelDetails(levelData);

            AddLevelSlots(levelData);

            AddButtonInitialize(levelData);

            AddButtonUpdate(levelData);
        }

        private void AddLevelDetails(LevelData levelData)
        {
            // Adds a object field that allow us to select a file of the type LevelConfiguration
            levelData.Configuration = EditorGUILayout.ObjectField("Level: ",
                levelData.Configuration, typeof(LevelConfiguration), false) as LevelConfiguration;

            // Adds a slider with min value of 1 and max value of 25
            levelData.Columns = EditorGUILayout.IntSlider("Columns: ",
                levelData.Columns, 1, 25);

            levelData.Rows = EditorGUILayout.IntSlider("Rows: ",
                levelData.Rows, 1, 25);

            SerializedProperty enemyGroups = serializedObject.FindProperty("EnemyGroups");
            EditorGUILayout.PropertyField(enemyGroups, new GUIContent("Enemy Groups"), true);

            serializedObject.ApplyModifiedProperties();
        }

        private void AddLevelSlots(LevelData levelData)
        {
            // Create a label with the desired text
            EditorGUILayout.LabelField("Level Item per position:");

            for (int x = 0; x < levelData.Rows; x++)
            {
                // Starts the horizontal layout so the any object added next will be aligned to the right
                // instead of the next line
                GUILayout.BeginHorizontal();

                for (int y = 0; y < levelData.Columns; y++)
                {
                    // Finds the level slot for the coodinate X, Y
                    // If not found a new object is created, so it is never null
                    LevelSlot slot = FindLevelSlot(levelData.Slots, x, y);
                    // Adds a dropdown list with the LevelItemType values and the current one selected
                    slot.ItemType = (LevelItemType)EditorGUILayout.EnumPopup(slot.ItemType);
                }

                // Once all objects are added horizontally, we end the layout so in the next loop
                // they will be added as a new line
                GUILayout.EndHorizontal();
            }
        }

        private LevelSlot FindLevelSlot(List<LevelSlot> slots, int x, int y)
        {
            // Try to find a level slot with the X and Y 
            LevelSlot slot = slots.Find(i => i.Coordinates.x == x &&
                                             i.Coordinates.y == y);
            // If not found, the object will be null and a new one is created and added to the list
            if (slot == null)
            {
                slot = new LevelSlot(LevelItemType.None, new Vector2Int(x, y));
                slots.Add(slot);
            }

            return slot;
        }

        private void AddButtonInitialize(LevelData levelData)
        {
            // Only executes the Initialize() method if the button is pressed
            if (GUILayout.Button("Initialize"))
            {
                Initialize(levelData);
            }
        }

        private void AddButtonUpdate(LevelData levelData)
        {
            // Only executes the following methods if the button is pressed
            if (GUILayout.Button("Update"))
            {
                // Mark the ScriptableObject levelData as changed
                EditorUtility.SetDirty(levelData);
                // Save all modified objects (levelData in this case)
                AssetDatabase.SaveAssets();
                // Refresh Unity and updates all modified objects
                AssetDatabase.Refresh();
            }
        }

        private void Initialize(LevelData levelData)
        {
            // Clear all data previously set to start a new one based on the update number of rows and columns
            levelData.Slots.Clear();

            for (int x = 0; x < levelData.Rows; x++)
            {
                for (int y = 0; y < levelData.Columns; y++)
                {
                    // For each row and column creates a new level slot with the None type
                    LevelSlot levelSlot = new LevelSlot(LevelItemType.None, new Vector2Int(x, y));
                    // And adds to the slots list
                    levelData.Slots.Add(levelSlot);
                }
            }
        }
    }
}
#endif
