using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SlimeState
{
    IDLE,
    WALK,
    ATTACK,
    STAGGER
}
public class SlimeDeath : MonoBehaviour
{
   public SlimeState currentState;
    public FloatValue maxHealth;
    public float health;
    
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
   

   
    public Camera cam;
  
   
    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject bulletPrefab;
  

    // Update is called once per frame
  
   
    




 



   

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            DeathEffect1();
            DeathEffect2();
            DeathEffect3();
            this.gameObject.SetActive(false);
        }
    }

   

    private void DeathEffect()
    {
        if (deathEffect != null)
        {GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

       // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
        }
    }
    private void DeathEffect1()
    {
        if (deathEffect != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint.rotation);
     

        }
    }
    private void DeathEffect2()
    {
        if (deathEffect != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint2.position, firePoint.rotation);

           

        }
    }
    private void DeathEffect3()
    {
        if (deathEffect != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint3.position, firePoint.rotation);
           
        }
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = SlimeState.IDLE;
            myRigidbody.velocity = Vector2.zero;
        }
    
}






}
