using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIntersection : PlanesIntersection
{
    private void Update()
    {   
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Vector3 intersectionPoint = ray.GetPoint(hit.distance);
                GetPlaneIntersection(intersectionPoint);
            }
        }
    }

}
