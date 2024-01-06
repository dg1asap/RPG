using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : interiactive
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private AudioSource powerUpSound;

    [SerializeField]
    private float soundMultiplier = 2.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("attack") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {     
                powerUpSound = GameObject.FindWithTag("ksiezniczka_song").GetComponent<AudioSource>();
                if (powerUpSound != null)
                {
                    powerUpSound.volume = Mathf.Clamp01(powerUpSound.volume * soundMultiplier);
                    powerUpSound.Play();
                }
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();

            playerInRange = false;
            dialogBox.SetActive(false);

        }
    }
}
