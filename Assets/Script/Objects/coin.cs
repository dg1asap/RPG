using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : PowerUp
{
    public Inventory playerInventory;
    private AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerupSignal.Raise();
            coinSound = GameObject.FindWithTag("coin_song").GetComponent<AudioSource>();
            coinSound.Play();
            Destroy(this.gameObject);
        }
    }

}
