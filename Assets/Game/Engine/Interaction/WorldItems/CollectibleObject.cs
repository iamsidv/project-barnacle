using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class CollectibleObject : BaseInteractableWorldItem
    {
        [SerializeField] private string collectibleId;

        public override void OnInteract()
        {
            Debug.Log($"Collectible Object {collectibleId}");
        }
    }
}