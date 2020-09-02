using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorType
{
key,
enemy,
button
}

public class Door : interiactive
{[Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    
    private void Update()
    {
        if(Input.GetButtonDown("attack"))
        {
            if(playerInRange && thisDoorType ==DoorType.key)
            {
                if (playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();


                }
            }
        }
    }
    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;

    }
    public void Close()
    {


    }
}
