using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientsManager : MonoBehaviour
{
    [SerializeField] private int numberOfPatients;
    [SerializeField] private float cooldown;
    [SerializeField] private List<GameObject> patientPrefab = new List<GameObject>();

    private float lastSpawnTime;

    private void Start()
    {
        SpawnPatient();
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (lastSpawnTime + cooldown <= Time.time && numberOfPatients > 0 && BunksManager.instance.ThereAreEmptyBeds()) 
        {
            numberOfPatients--;
            SpawnPatient();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnPatient()
    {
        Instantiate(patientPrefab[Random.Range(0, patientPrefab.Count)], transform.position, Quaternion.identity);
    }
}
