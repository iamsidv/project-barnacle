using Game.Configs;
using Game.Engine;
using Game.Engine.Interaction;
using Game.Engine.Interaction.WorldItems;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI.Crafting
{
    public class CraftItemsView : BaseView
    {
        [SerializeField] private CraftingSection craftingSection;
        [SerializeField] private InventorySection inventorySection;

        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnClear;
        [SerializeField] private Button btnExit;
        [SerializeField] private Workbench workbench;

        public CraftingSection CraftingSection => craftingSection;
        public InventorySection InventorySection => inventorySection;

        public override void OnScreenEnter()
        {
            btnContinue.onClick.AddListener(CraftMaterials);
            btnClear.onClick.AddListener(ClearCraftingOptions);
            btnExit.onClick.AddListener(OnExit);
            btnExit.interactable = true;

            craftingSection.Init(this);
            inventorySection.Init(this);

            craftingSection.Refresh();
            inventorySection.Refresh();

            GameManager.Instance.SetHeroVisibility(false);
        }

        private void OnExit()
        {
            OnScreenExit();
            gameObject.SetActive(false);
        }

        public override void OnScreenExit()
        {
            btnContinue.onClick.RemoveAllListeners();
            btnClear.onClick.RemoveAllListeners();
            btnExit.onClick.RemoveAllListeners();
            btnExit.interactable = false;

            GameManager.Instance.SetHeroVisibility(true);
        }

        private void ClearCraftingOptions()
        {
            craftingSection.ResetSlots();
            inventorySection.ResetSlot();
        }

        private void CraftMaterials()
        {
            craftingSection.CraftItem();
        }

        public void OnCraftingComplete(CraftingRuleSet craftedRuleSet, int spawnIndex)
        {
            OnExit();
            workbench.SpawnCraftedItem(craftedRuleSet.CratedItemPrefab, spawnIndex);
        }

        public void BindWorldItemToView(BaseInteractableWorldItem worldItem)
        {
            workbench = (Workbench)worldItem;
        }
    }
}