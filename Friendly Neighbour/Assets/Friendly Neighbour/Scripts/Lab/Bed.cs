using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MouseScript
{
    public PlayerController player;
    public int bedIndex;
    public bool isOccupied;
    private Collider interactionTrigger;
    private Patient patient;
    private BunksManager bunksManager;
    private bool onTrigger = false;

    private void Start()
    {
        interactionTrigger = GetComponent<Collider>();
    }

    private void Update() {
        if (onTrigger && isOccupied) 
        {
            if (player.isCarryMedicine && GetMouseInput(interactionTrigger)) {
                Debug.Log("Bed " + bedIndex + " interacted");
                player.HideBottle();
                StartCoroutine(patient.GoToExit());
                FreeBed();
            }
        }
    }

    public void OccupyBed(Patient _patient)
    {
        isOccupied = true;
        gameObject.GetComponent<Renderer>().material = BunksManager.instance.occupiedMaterial;
        patient = _patient;
        BunksManager.instance.numberOfOccupiedBeds++;
    }

    public void FreeBed()
    {
        isOccupied = false;
        gameObject.GetComponent<Renderer>().material = BunksManager.instance.freeMaterial;
        SetInteractionTrigger();
        patient = null;
        BunksManager.instance.numberOfOccupiedBeds--;
    }

    public void SetInteractionTrigger()
    {
        interactionTrigger.enabled = !interactionTrigger.enabled;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onTrigger = false;
        }
    }
}
