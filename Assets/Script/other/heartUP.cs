using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartUP : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;
    private AudioSource powerUpSound;

    [SerializeField]
    private float soundMultiplier = 2.0f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")&& heartContainers.RuntimeValue <=4)
        {
            heartContainers.RuntimeValue += 1;
            
            playerHealth.RuntimeValue=   heartContainers.RuntimeValue * 2;
            powerupSignal.Raise();
            powerUpSound = GameObject.FindWithTag("powerUp_song").GetComponent<AudioSource>();

            if (powerUpSound != null)
            {
                powerUpSound.volume = Mathf.Clamp01(powerUpSound.volume * soundMultiplier);
                powerUpSound.Play();
            }

            Destroy(this.gameObject);

        }
        else
        {
            powerUpSound = GameObject.FindWithTag("powerUp_song").GetComponent<AudioSource>();
            if (powerUpSound != null)
            {
                powerUpSound.volume = Mathf.Clamp01(powerUpSound.volume * soundMultiplier);
                powerUpSound.Play();
            }
            Destroy(this.gameObject);
        }

    }
}
