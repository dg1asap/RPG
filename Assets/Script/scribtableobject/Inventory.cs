using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Inventory : ScriptableObject
{//// ///////////////////

    public Signal powerupSignal;
   
    /// ////////////////////////
 
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public void AddItem(Item itemToAdd)
    {
        if(itemToAdd.isKey)
        {
            numberOfKeys++;

        }
        if(itemToAdd.iscoins)
        {
            coins = coins + 100;
            powerupSignal.Raise();
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }

    }
}
