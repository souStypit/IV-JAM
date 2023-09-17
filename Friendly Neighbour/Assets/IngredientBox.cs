using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    [SerializeField] private Collider interactionObject;
    [SerializeField] private Item itemToGive;

    private bool onTrigger;

    private void Update()
    {
        if (onTrigger && MouseScript.GetMouseInput(interactionObject))
        {
            Player.instance.TakeItem(itemToGive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }
}
