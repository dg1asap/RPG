using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;

    public float knockTime;
    public float damage;
    public int whichMobAttackSound = 0;
    private AudioSource powerUpSound;

    [SerializeField]
    private float soundMultiplier = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable")&& this.gameObject.CompareTag("Player") )
        {
            other.GetComponent<pot>().Smash();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Slime")||(other.gameObject.CompareTag("Player")&&!this.gameObject.CompareTag("frie")))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<enemy>().currentState = enemyState.STAGGER;
                    other.GetComponent<enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Slime") && other.isTrigger)
                {
                    hit.GetComponent<SlimeDeath>().currentState = SlimeState.STAGGER;
                    other.GetComponent<SlimeDeath>().Knock(hit, knockTime, damage);
                }
                
                if(other.gameObject.CompareTag("Player")&&!this.gameObject.CompareTag("frie"))
                { 
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.STAGGER)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.STAGGER;

                        switch (whichMobAttackSound)
                        {
                            case 0:
                                powerUpSound = GameObject.FindWithTag("log_song").GetComponent<AudioSource>();
                                break;
                            case 1:
                                powerUpSound = GameObject.FindWithTag("czacha_mnich_mag_song").GetComponent<AudioSource>();
                                break;
                            case 2:
                                powerUpSound = GameObject.FindWithTag("czarny_kosarz_song").GetComponent<AudioSource>();
                                break;
                            case 3:
                                powerUpSound = GameObject.FindWithTag("demon_song").GetComponent<AudioSource>();
                                break;
                            case 4:
                                powerUpSound = GameObject.FindWithTag("goblin_song").GetComponent<AudioSource>();
                                break;
                            case 5:
                                powerUpSound = GameObject.FindWithTag("mnich_song").GetComponent<AudioSource>();
                                break;
                            case 6:
                                powerUpSound = GameObject.FindWithTag("oko_song").GetComponent<AudioSource>();
                                break;
                            case 7:
                                powerUpSound = GameObject.FindWithTag("robot_song").GetComponent<AudioSource>();
                                break;
                            case 8:
                                powerUpSound = GameObject.FindWithTag("slime_song").GetComponent<AudioSource>();
                                break;
                            case 9:
                                powerUpSound = GameObject.FindWithTag("zywiolak_song").GetComponent<AudioSource>();
                                break;
                            default:
                                powerUpSound = GameObject.FindWithTag("log_song").GetComponent<AudioSource>();
                                break;
                        }
                       
                        if (powerUpSound != null)
                        {
                            powerUpSound.volume = Mathf.Clamp01(powerUpSound.volume * soundMultiplier);
                            powerUpSound.Play();
                        }
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
