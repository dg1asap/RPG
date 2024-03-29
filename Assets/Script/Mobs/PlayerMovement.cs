﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AttackProcess;

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
    private GameMaster gm;
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 direction;
    private Animator animator;
    float zdrowie;
    public FloatValue Health;
    public float speed;
    public Signal playerHealthSignal;
    public FloatValue heartContainers;
    public VectorValue startingPosition;

    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;
    
    private AudioSource powerUpSound;
    public FixedJoystick joystick;

    [SerializeField]
    private float soundMultiplier = 2.0f;

    void Start()
    {zdrowie=Health.RuntimeValue;
        Application.targetFrameRate = 60;
        
        currentState = PlayerState.WALK;
        myRigidbody = GetComponent<Rigidbody2D>();
      
        animator = GetComponent<Animator>();
        setSpawnPoint();
    }

    private void setSpawnPoint()
    {
        setStartingPosition();
        rotatePlayerToSouth();
    }

    private void setStartingPosition()
    {
        transform.position = startingPosition.initialValue;
    }

    private void rotatePlayerToSouth()
    {
        rotatePlayerAtXY(0, -1);
    }

    private void rotatePlayerAtXY(int x, int y)
    {
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
    }

    void Update()
    {
        playerHealthSignal.Raise();
        if(canControlled())
        {
            steer();
            UpdateJoystickMovement();
        }
    }

    private bool canControlled()
    {
        return !isInInteract();
    }

    private bool isInInteract()
    {
        return currentState == PlayerState.INTERACT;
    }

    private void steer()
    {
        if(canAttack() && isPressedAttackButton())
            attack();
        else if(canMove())
        {
            // setDirection();
            UpdateAnimationAndMove();
        }
    }

    private void setDirection()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }
   
    private bool canAttack()
    {
        return !isInAttackingOrStagger();
    }
    
    private bool isInAttackingOrStagger()
    {
        return currentState == PlayerState.ATTACK || currentState == PlayerState.STAGGER;
    }
   
    private bool isPressedAttackButton()
    {
        return AttackFactory.canProduce();
    }

    private bool canMove()
    {
        return currentState == PlayerState.WALK || currentState == PlayerState.IDLE;
    }

    public void attack()
    {
        Attack a = AttackFactory.make();
        StartCoroutine(meleeAttack());
    } 
   
    private IEnumerator meleeAttack()
    {
        startMeleeAttack();
        yield return null;
        yield return StartCoroutine(
            endMeleeAttackAndTakeCooldown(.3f));
    }

    private void startMeleeAttack()
    {
        setONattack();
        animator.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
    }

    private void setONattack(){
        currentState = PlayerState.ATTACK;
    }

    private IEnumerator endMeleeAttackAndTakeCooldown(float cooldown)
    {
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(cooldown);

        if(canControlled()){
            setONmove();
        }
    }

    private void setONmove(){
        currentState = PlayerState.WALK;
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

    private void UpdateJoystickMovement()
    {   
        direction = new Vector3(joystick.Horizontal, joystick.Vertical, 0f).normalized;
        Debug.Log("Joystick Direction: " + direction);
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
        
        powerUpSound = GameObject.FindWithTag("player_dmg").GetComponent<AudioSource>();
        if (powerUpSound != null)
        {
            powerUpSound.volume = Mathf.Clamp01(powerUpSound.volume * soundMultiplier);
            powerUpSound.Play();
        }
        
        playerHealthSignal.Raise();
        if (Health.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Health.RuntimeValue = heartContainers.RuntimeValue*2;

            playerHealthSignal.Raise();
            // this.gameObject.SetActive(false);
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
