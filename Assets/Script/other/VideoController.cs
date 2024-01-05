using UnityEngine;

public class VideoController : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 1.0f; 

    void Start()
    {
        audioSource.loop = true; 
        audioSource.volume = volume; 
        audioSource.Play();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            volume = Mathf.Clamp01(volume + 0.1f); 
            audioSource.volume = volume;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            volume = Mathf.Clamp01(volume - 0.1f); 
            audioSource.volume = volume;
        }
    }
}
