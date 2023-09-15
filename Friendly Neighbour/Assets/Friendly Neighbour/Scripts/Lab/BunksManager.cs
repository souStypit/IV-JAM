using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BunksManager : MonoBehaviour
{
    public static BunksManager instance;
    [HideInInspector] public int numberOfOccupiedBeds = 0;
    public Material freeMaterial, occupiedMaterial;

    private List<Bed> allBeds = new List<Bed>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Bed[] beds = GetComponentsInChildren<Bed>();
        allBeds.AddRange(beds);
        allBeds.Sort((a, b) => a.bedIndex.CompareTo(b.bedIndex));
    }

    public Bed GetFreeBed(Patient _patient)
    {
        if (allBeds == null || numberOfOccupiedBeds == allBeds.Count) return null;

        foreach (var bed in allBeds)
        {
            if (!bed.isOccupied)
            {
                bed.OccupyBed(_patient);
                return bed;
            }
        }

        return null;
    }

    public bool ThereAreEmptyBeds()
    {
        return numberOfOccupiedBeds < allBeds.Count;
    }
}
