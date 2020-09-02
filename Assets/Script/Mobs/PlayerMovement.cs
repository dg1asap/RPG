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

// TODO : zmiana nazwy klasy na Player
public class PlayerMovement : MonoBehaviour
{
    public Camera cam;

    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 direction;
    private Animator animator;

    public FloatValue Health;
    public float speed;
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

        //TODO usunąć sprzężenie czasowe getComonent<Animator> i setSpawnPoint()
        animator = GetComponent<Animator>();
        setSpawnPoint();
    }

    private void setSpawnPoint(){
        setStartingPosition();
        rotatePlayerToSouth();
    }

    private void setStartingPosition(){
        transform.position = startingPosition.initialValue;
    }

    private void rotatePlayerToSouth(){
        rotatePlayerAtXY(0, -1);
    }

    private void rotatePlayerAtXY(int x, int y){
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
    }

    void Update()
    {
        returnIfPlayerInteract();

        setThePlayerDirection();

        if(isCapableOfAttack() && isPressedAttackButton())
            StartCoroutine(AttackCo());
        else if(isCapableOfMove())
            UpdateAnimationAndMove();
    }

    private void returnIfPlayerInteract(){
        if(currentState == PlayerState.INTERACT)
            return;
    }
  
    private void setThePlayerDirection(){
        direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }
   
    private bool isCapableOfAttack(){
        return isInAttackingOrStagger();
    }
    
    private bool isInAttackingOrStagger(){
        return currentState != PlayerState.ATTACK && currentState != PlayerState.STAGGER;
    }
   
    private bool isPressedAttackButton(){
        return Input.GetButtonDown("attack");
    }

    private bool isCapableOfMove(){
        return currentState == PlayerState.WALK || currentState == PlayerState.IDLE;
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
        if(direction != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX",direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving",false);
        }
    }
    
    void MoveCharacter()
    {
        direction.Normalize();
        myRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
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


// class Movement{


// }
