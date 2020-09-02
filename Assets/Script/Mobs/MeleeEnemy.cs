using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : log
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void CheckDistance()
    {
      
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if ( currentState == enemyState.WALK|| currentState == enemyState.IDLE
                && currentState != enemyState.STAGGER)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(enemyState.WALK);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == enemyState.WALK
                  && currentState != enemyState.STAGGER)
            {
                StartCoroutine(AttackCo());
            }
        }
    }
    public IEnumerator AttackCo()
    {
        currentState = enemyState.ATTACK;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = enemyState.WALK;
        anim.SetBool("attack", false);
    }

    private void ChangeState(enemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}