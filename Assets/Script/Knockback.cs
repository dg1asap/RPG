using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;

    public float knockTime;
    public float damage;

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
                { if (other.GetComponent<PlayerMovement>().currentState != PlayerState.STAGGER)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.STAGGER;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }

             
                }

        }
    
    }

   

}
