using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript
{
    public static bool GetMouseInput(Collider coll)
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (coll.Raycast(ray, out hit, 100.0f)) return true;
        }
        
        return false;
    } 
}
