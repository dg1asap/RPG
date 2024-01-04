using UnityEngine;

public class VideoController : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource.loop = true; // Ustawia zapętlenie dźwięku
        audioSource.Play();
    }
}
