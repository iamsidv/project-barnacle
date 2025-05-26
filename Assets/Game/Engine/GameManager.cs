using Game.Engine.Interaction.WorldItems;
using Game.Engine.Movement;
using UnityEngine;

namespace Game.Engine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject heroControl;
        [SerializeField] private PlayerCameraController followCamera;
        [SerializeField] private CollectibleObject[] collectibleObjects;

        [SerializeField] private House house;
        
        private void Awake()
        {
            Instance = this;

            if (heroControl == null)
            {
                heroControl = FindAnyObjectByType<HeroMovement>().gameObject;
                followCamera = FindAnyObjectByType<PlayerCameraController>();
            }

            collectibleObjects =
                FindObjectsByType<CollectibleObject>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);

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
                followCamera.ChangeCameraOrientation(true);
            }
        }

        public void ExitHouse()
        {
            if (house)
            {
                house.enterCollider.SetActive(true);
                house.exitCollider.SetActive(false);
                house.ceiling.SetActive(true);
                followCamera.ChangeCameraOrientation(false);
            }
        }
    }
}