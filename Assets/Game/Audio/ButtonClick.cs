using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource; // Объект, который проигрывает звук
    public AudioClip clickSound;    // Сам звуковой файл

    void Start()
    {
        // Получаем компонент кнопки на том же объекте
        Button button = GetComponent<Button>();

        // Подписываемся на клик
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
