﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreasureChest : interiactive
{[Header("Contents")]
    public Item contents;
    public Inventory playerInventory;

    public bool isOpen;
    public boolValue storeOpen;
    [Header("Signal and Dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    [Header("Animation")]

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        isOpen = storeOpen.RuntimeValue;
        if (isOpen)
        {

            anim.SetBool("opened", true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if(!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();

            }
           
        }


    }
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
       
        context.Raise();
isOpen = true;
        anim.SetBool("opened", true);
        storeOpen.RuntimeValue = isOpen;


    }

    public void ChestAlreadyOpen()
    {
            dialogBox.SetActive(false);
            
            raiseItem.Raise();
            
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();

            playerInRange = true;


        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();

            playerInRange = false;


        }

    }
}
