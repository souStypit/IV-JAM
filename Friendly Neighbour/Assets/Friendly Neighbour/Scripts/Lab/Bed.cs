using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public int bedIndex;
    public bool isOccupied;

    private Collider interactionTrigger;
    private Patient patient;
    private BunksManager bunksManager;

    private void Start()
    {
        interactionTrigger = GetComponent<Collider>();
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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q) && other.gameObject.GetComponent<PlayerController>()) {
            Debug.Log("Bed " + bedIndex + " interacted");
            StartCoroutine(patient.GoToExit());
            FreeBed();
        }
    }
}
