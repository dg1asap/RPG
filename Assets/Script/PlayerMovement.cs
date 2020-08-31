using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    WALK,
    ATTACK,
    INTERACT,
    STAGGER,
    IDLE
}

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;

    public float   speed;

    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    public FloatValue Health;
    public Signal playerHealthSignal;

    public VectorValue startingPosition;

    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;

    Vector2 mousePos;

    void Start()
    {
        Application.targetFrameRate = 60;
        currentState = PlayerState.WALK;
        myRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = startingPosition.initialValue;
    }

    void Update()
    {
        if(currentState == PlayerState.INTERACT)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.ATTACK 
            && currentState != PlayerState.STAGGER)
        {
           StartCoroutine(AttackCo());
        }
        else  if (currentState == PlayerState.WALK||currentState ==PlayerState.IDLE)
        {
            UpdateAnimationAndMove();
        }
    }
   
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.INTERACT)
        {
            currentState = PlayerState.WALK;
        }
    }
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.INTERACT)
            {
                animator.SetBool("receiveitem", true);
                currentState = PlayerState.INTERACT;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveitem", false);
                currentState = PlayerState.IDLE;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX",change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving",false);
        }
    }
    
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
   
   
  
    public void Knock(float knockTime,float damage)
    {
        Health.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (Health.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
  
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();

        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.IDLE;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
