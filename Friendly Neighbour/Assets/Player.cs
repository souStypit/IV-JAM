using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool isCarrying { get; private set; }
    public Item carryingItem { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }

    public void TakeItem(Item item)
    {
        if (!isCarrying) 
        {
            isCarrying = true;
            carryingItem = item;
            UIController.instance.SetItemImage(item);
        }
    }

    public void RemoveItem()
    {
        isCarrying = false;
        carryingItem = null;
        UIController.instance.SetEmptyImage();
    }
}
