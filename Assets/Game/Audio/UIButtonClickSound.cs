using UnityEngine;
using UnityEngine.UI;

public class UIButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private static AudioSource sharedAudioSource;

    void Start()
    {
        // Ищем и сохраняем AudioSource один раз
        if (sharedAudioSource == null)
        {
            GameObject soundObj = GameObject.Find("UIButtonSound");
            if (soundObj != null)
            {
                sharedAudioSource = soundObj.GetComponent<AudioSource>();
            }
        }

        // Подписываемся на клик
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (sharedAudioSource == null)
        {
            Debug.LogWarning("AudioSource не найден!");
            return;
        }

        if (!sharedAudioSource.enabled || !sharedAudioSource.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("AudioSource выключен или объект не активен");
            return;
        }

        if (clickSound != null)
        {
            sharedAudioSource.PlayOneShot(clickSound);
        }
    }
}
