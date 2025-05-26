using Game.Engine.Actions;
using Game.Profile;
using UnityEngine;

namespace Game.Engine.Interaction.WorldItems
{
    public class CollectibleObject : BaseInteractableWorldItem
    {
        [SerializeField] private string collectibleId;
        [SerializeField] private GameObject collectEffect;
        public override void OnInteract()
        {
            Debug.Log($"Collectible Object {collectibleId}");

            IPlayerAction action = new AddItemToInventoryAction(new InventoryItem(collectibleId));
            ActionResult result = action.Execute(GameEngine.Context);
            if (result == ActionResult.Success)
            {
                if (collectEffect)
                {
                    GameObject go = Instantiate(collectEffect, transform.position, Quaternion.identity);
                    Destroy(go, 2f);
                }
                gameObject.SetActive(false);
            }
        }
    }
}