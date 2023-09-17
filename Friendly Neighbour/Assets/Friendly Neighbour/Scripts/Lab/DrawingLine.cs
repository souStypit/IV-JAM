using EzySlice;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TMPro;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject cutPlanePrefab;
    [SerializeField] private LayerMask whatIsLayer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLine();
        }
    }

    private void StartDrawing()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, GetMouseWorldPosition());
        lineRenderer.SetPosition(1, GetMouseWorldPosition());
    }

    private void StopDrawing()
    {
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        Vector3 center = (endPoint + startPoint) / 2;
        Vector3 direction = (endPoint - startPoint).normalized;
        Vector3 planeScale = new Vector3((endPoint - startPoint).magnitude / 10f, cutPlanePrefab.transform.localScale.y, cutPlanePrefab.transform.localScale.z);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        cutPlanePrefab.transform.localScale = planeScale;
        GameObject cutPlane = Instantiate(cutPlanePrefab, center, Quaternion.Euler(0, 0, angle));

        RaycastHit hit;
        if(Physics.Raycast(cutPlane.transform.position, cutPlane.transform.forward, out hit))
        {
            var obj = hit.collider.gameObject;
            SlicedHull slices = obj.Slice(cutPlane.transform.position, cutPlane.transform.up);

            GameObject upperHull = slices.CreateUpperHull(obj, obj.GetComponent<Renderer>().material);
            GameObject lowerHull = slices.CreateLowerHull(obj, obj.GetComponent<Renderer>().material);
            if (upperHull.GetComponent<Renderer>().bounds.center.y < lowerHull.GetComponent<Renderer>().bounds.center.y)
            {
                GameObject tmp = upperHull;
                upperHull = lowerHull;
                lowerHull = tmp;
            }
            SetupSlicedComponents(upperHull, lowerHull, obj, cutPlane.transform.position);

            Destroy(obj);
        }

        lineRenderer.positionCount = 0;
    }

    private void SetupSlicedComponents(GameObject upperHull, GameObject lowerHull, GameObject original, Vector3 cutPosition)
    {
        Rigidbody upperRb = upperHull.AddComponent<Rigidbody>();
        Rigidbody lowerRb = lowerHull.AddComponent<Rigidbody>();

        MeshCollider upperCollider = upperHull.AddComponent<MeshCollider>();
        MeshCollider lowerCollider = lowerHull.AddComponent<MeshCollider>();
        upperCollider.convex = true;
        lowerCollider.convex = true;

        if (cutPosition.y > original.GetComponent<Renderer>().bounds.center.y)
        {
            lowerRb.useGravity = false;
            lowerRb.isKinematic = true;
            lowerRb.freezeRotation = true;
            //upperRb.AddForce((upperHull.GetComponent<Renderer>().bounds.center - cutPosition).normalized * 1000);
        } else
        {
            upperRb.useGravity = false;
            upperRb.isKinematic = true;
            upperRb.freezeRotation = true;
            //lowerRb.AddForce(lowerHull.GetComponent<Renderer>().bounds.center - cutPosition);
        }
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(1, GetMouseWorldPosition());
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Задайте значение z, чтобы линии были видны на сцене
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
