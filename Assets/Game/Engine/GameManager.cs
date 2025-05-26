using Game.Engine.Movement;
using UnityEngine;

namespace Game.Engine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject heroControl;
        [SerializeField] private GameObject followCamera;

        private void Awake()
        {
            Instance = this;

            if (heroControl == null)
            {
                heroControl = FindAnyObjectByType<HeroMovement>().gameObject;
                followCamera = FindAnyObjectByType<PlayerCameraController>().gameObject;
            }
        }
        

        public void SetHeroVisibility(bool active)
        {
            heroControl.SetActive(active);
        }
    }
}
