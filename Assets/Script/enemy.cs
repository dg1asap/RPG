﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum enemyState { 
    idle,
    walk,
    attack,
    stagger
}


public class enemy : MonoBehaviour
{
    public enemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyname;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public lootTable thisLoot;

    private void Awake()
    {
        health = maxHealth.initialValue;

    }
    // Start is called before the first frame update
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);


        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);


            }
        }
    }




    private void DeathEffect()
    {if(deathEffect != null)
        {
            GameObject effect =Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime,float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));

        TakeDamage(damage);

    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
          currentState = enemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }

    }

}
