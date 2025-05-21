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
    }

    [System.Serializable]
    public class CollectibleItem
    {
        [SerializeField] private string id;
        [SerializeField] private GameObject prefab;
    }

    [System.Serializable]
    public class CraftingRuleSet
    {
        [SerializeField] private List<string> collectableItem;
        [SerializeField] private string result;
        [SerializeField] private GameObject prefab;
    }
}
