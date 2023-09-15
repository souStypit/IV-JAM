using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingScript : MouseScript
{
    public GameObject button;
    private Collider coll;
    private bool onTrigger = false;

    private void Start() {
        button.SetActive(false);
    }
    private void Update()
    {
        if (onTrigger) 
        {
            if (GetMouseInput(coll)) {
                Debug.Log("HAAAAA");
                button.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("CookingTable")) {
            onTrigger = true;
            coll = other;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("CookingTable")) {
            onTrigger = false;
            coll = null;
            button.SetActive(false);
        }
    }
}
