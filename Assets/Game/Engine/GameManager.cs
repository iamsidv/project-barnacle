using System;
using Game.Engine.Actions;
using Game.Engine.Interaction.WorldItems;
using Game.Engine.Movement;
using Game.Profile;
using Game.UI.Hud;
using UnityEngine;

namespace Game.Engine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject heroControl;
        [SerializeField] private PlayerCameraController followCamera;
        [SerializeField] private Camera houseCamera;
        private CollectibleObject[] _collectibleObjects;

        [SerializeField] private House house;

        private void Awake()
        {
            Instance = this;

            if (heroControl == null)
            {
                heroControl = FindAnyObjectByType<HeroMovement>().gameObject;
                followCamera = FindAnyObjectByType<PlayerCameraController>();
            }

            _collectibleObjects = FindObjectsByType<CollectibleObject>
                (FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);

            house = FindAnyObjectByType<House>();
        }

        public void SetHeroVisibility(bool active)
        {
            heroControl.SetActive(active);
        }

        public void EnterHouse()
        {
            if (house)
            {
                house.enterCollider.SetActive(false);
                house.exitCollider.SetActive(true);
                house.ceiling.SetActive(false);
                //followCamera.ChangeCameraOrientation(true);
                followCamera.gameObject.SetActive(false);
                houseCamera.gameObject.SetActive(true);
            }
        }

        public void ExitHouse()
        {
            if (house)
            {
                house.enterCollider.SetActive(true);
                house.exitCollider.SetActive(false);
                house.ceiling.SetActive(true);
                //followCamera.ChangeCameraOrientation(false);
                followCamera.gameObject.SetActive(true);
                houseCamera.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                IPlayerAction action = new AddItemToInventoryAction(new InventoryItem("jarlid"));
                ActionResult result = action.Execute(GameEngine.Context);
                if (result == ActionResult.Success)
                {
                    HudView.Instance.DisplayText($"'{"jarlid"}' added to inventory");
                }
                else
                {
                    HudView.Instance.DisplayText("Inventory Full!");
                }
            }
        }
    }
}