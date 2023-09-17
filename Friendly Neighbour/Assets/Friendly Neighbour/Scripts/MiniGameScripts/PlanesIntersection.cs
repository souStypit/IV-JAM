using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesIntersection : PlaneGenerator
{
    public MeshFilter meshFIlter;
    private Vector3 point_1 = Vector3.zero;
    public void GetPlaneIntersection(Vector3 intersectionPoint)
    {
        if (point_1 == Vector3.zero) {
            point_1 = intersectionPoint;
            return;
        }
        GeneratePlane(meshFIlter, point_1, intersectionPoint);
        point_1 = Vector3.zero;
    }
}
