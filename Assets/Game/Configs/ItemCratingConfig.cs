using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "ItemCratingConfig.asset", menuName = "Configs/Crafting", order = -1)]
    public class ItemCratingConfig : ScriptableObject
    {
        [SerializeField] private List<CollectibleItem> collectibles;
        [SerializeField] private List<CraftingRuleSet> craftingRules;

        public List<CollectibleItem> Collectibles => collectibles;
        public List<CraftingRuleSet> CraftingRules => craftingRules;

        
        private void DoSth()
        {
            foreach (var item in CraftingRules)
            {
                item.Validate();
            }
            
            foreach (CollectibleItem item in Collectibles)
            {
                item.Validate();
            }
        }
    }

    [System.Serializable]
    public class CollectibleItem
    {
        [SerializeField] private string id;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite icon;

        public string Id => id;
        public GameObject Prefab => prefab;
        public Sprite Icon => icon;

        public void Validate()
        {
            // id = id.ToLower();
        }
    }

    [System.Serializable]
    public class CraftingRuleSet
    {
        [SerializeField] private List<string> collectableItem;
        [SerializeField] private string result;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite icon;

        public string ResultId()
        {
            return string.Join("~", collectableItem);
        }
        
        public void Validate()
        {
            // for (int i = 0; i < collectableItem.Count; i++)
            // {
            //     collectableItem[i] = collectableItem[i].ToLower();
            // }
            //
            // result = result.ToLower();
            
        }
    }
}
