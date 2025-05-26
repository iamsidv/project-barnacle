using System;
using UnityEngine;

namespace Game.Engine
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource buttonClickSource;
        [SerializeField] private AudioSource itemCollectSource;
        
        private void Awake()
        {
            Instance = this;
        }

        public void PlayButtonClickSfx()
        {
            buttonClickSource.Play();
        }
        
        public void PlayItemCollectSfx()
        {
            itemCollectSource.Play();
        }
    }
}
