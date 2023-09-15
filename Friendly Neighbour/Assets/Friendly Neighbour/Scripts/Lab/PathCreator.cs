using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PathToBed
{
    [Header("Nodes To Bed")]
    public List<Transform> path;
}

public class PathCreator : MonoBehaviour
{
    public static PathCreator instance;
    [SerializeField] private List<PathToBed> paths;

    private void Awake()
    {
        instance = this;
    }

    public List<Transform> GetPath(int bedIndex)
    {
        return paths[bedIndex].path;
    }

}
