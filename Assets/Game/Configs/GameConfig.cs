using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "GameConfig.asset", menuName = "Configs/Main", order = -1)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int inventorySlots;
        [SerializeField] private ItemCratingConfig craftConfig;

        public ItemCratingConfig CraftConfig => craftConfig;
    }
}