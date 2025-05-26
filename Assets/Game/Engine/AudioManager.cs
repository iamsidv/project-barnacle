using System;
using UnityEngine;

namespace Game.Engine
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource buttonClickSource;
        [SerializeField] private AudioSource itemCollectSource;

        [SerializeField] private AudioSource mainThemeSource;
        [SerializeField] private AudioSource miniGameSource;
        
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

        public void PlayMinigameTheme(AudioClip clip)
        {
            if (clip != null)
            {
                mainThemeSource.Stop();
            }
            
            miniGameSource.Stop();
            miniGameSource.clip = clip;
            miniGameSource.loop = true;
            miniGameSource.Play();
        }

        public void StopMinigameTheme()
        {
            miniGameSource.Stop();
            mainThemeSource.Play();
        }
    }
}
