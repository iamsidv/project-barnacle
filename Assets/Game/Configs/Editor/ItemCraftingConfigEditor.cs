#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Game.Configs.Editor
{
    [CustomEditor(typeof(ItemCratingConfig))]
    public class ItemCraftingConfigEditor : UnityEditor.Editor
    {
        SerializedProperty collectiblesProp;
        SerializedProperty craftingRulesProp;

        List<string> collectibleIds = new();
        List<ReorderableList> craftingRuleLists = new();

        void OnEnable()
        {
            collectiblesProp = serializedObject.FindProperty("collectibles");
            craftingRulesProp = serializedObject.FindProperty("craftingRules");

            UpdateCollectibleIds();
            CreateCraftingRuleLists();
        }

        void UpdateCollectibleIds()
        {
            collectibleIds.Clear();
            for (int i = 0; i < collectiblesProp.arraySize; i++)
            {
                var collectible = collectiblesProp.GetArrayElementAtIndex(i);
                var idProp = collectible.FindPropertyRelative("id");
                collectibleIds.Add(idProp.stringValue);
            }
        }

        void CreateCraftingRuleLists()
        {
            craftingRuleLists.Clear();
            for (int i = 0; i < craftingRulesProp.arraySize; i++)
            {
                var ruleProp = craftingRulesProp.GetArrayElementAtIndex(i);
                var itemsProp = ruleProp.FindPropertyRelative("collectableItem");

                var list = new ReorderableList(serializedObject, itemsProp, true, true, true, true);

                int ruleIndex = i; // capture for closures

                list.drawHeaderCallback = rect => {
                    EditorGUI.LabelField(rect, $"Collectible Items (Rule {ruleIndex + 1})");
                };

                list.drawElementCallback = (rect, index, isActive, isFocused) => {
                    var itemProp = itemsProp.GetArrayElementAtIndex(index);
                    int currentIndex = Mathf.Max(0, collectibleIds.IndexOf(itemProp.stringValue));
                    int newIndex = EditorGUI.Popup(rect, currentIndex, collectibleIds.ToArray());
                    if (newIndex >= 0 && newIndex < collectibleIds.Count)
                    {
                        itemProp.stringValue = collectibleIds[newIndex];
                    }
                };

                craftingRuleLists.Add(list);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw collectibles
            EditorGUILayout.PropertyField(collectiblesProp, true);
            UpdateCollectibleIds(); // update in case of changes

            // Regenerate reorderable lists if rule count changed
            if (craftingRulesProp.arraySize != craftingRuleLists.Count)
            {
                CreateCraftingRuleLists();
            }

            // Draw each rule
            EditorGUILayout.LabelField("Crafting Rules", EditorStyles.boldLabel);
            for (int i = 0; i < craftingRulesProp.arraySize; i++)
            {
                var ruleProp = craftingRulesProp.GetArrayElementAtIndex(i);
                var resultProp = ruleProp.FindPropertyRelative("result");
                var prefabProp = ruleProp.FindPropertyRelative("prefab");

                EditorGUILayout.BeginVertical("box");

                // Reorderable list for collectableItem
                craftingRuleLists[i].DoLayoutList();

                // Result field
                EditorGUILayout.PropertyField(resultProp);
                EditorGUILayout.PropertyField(prefabProp);

                if (GUILayout.Button("Remove Rule"))
                {
                    craftingRulesProp.DeleteArrayElementAtIndex(i);
                    CreateCraftingRuleLists(); // refresh lists
                    break;
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Crafting Rule"))
            {
                craftingRulesProp.InsertArrayElementAtIndex(craftingRulesProp.arraySize);
                CreateCraftingRuleLists(); // refresh lists
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
