using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Materials.asset", menuName = "Configs/Collectable Items")]
    public class CollectibleItemConfig : ScriptableObject
    {
        public List<string> collectibleIds = new();
    }
}