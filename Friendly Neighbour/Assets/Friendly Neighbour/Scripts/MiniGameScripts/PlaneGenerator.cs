using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    private void Start()
    {
        GeneratePlane();
    }

    private void GeneratePlane()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Генерация вершин
        Vector3[] vertices = {
            new Vector3(0f,0f,0f), 
            new Vector3(10f,0f,0f),
            new Vector3(0f,10f,0f),
            new Vector3(10f,10f,0f)
        };
        mesh.vertices = vertices;

        // Генерация треугольников
        int[] triangles = {
            0, 2, 1,
            1, 2, 3
        };
    
        mesh.triangles = triangles;

        // Расчет нормалей и других свойств меша
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
    }

    public void GeneratePlane(MeshFilter meshFilter, Vector3 p1, Vector3 p2)
    {
        Debug.Log(p2);

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = {
            p1,
            new Vector3(10f,0f,0f),
            p2,
            new Vector3(10f,10f,0f)
        };
        mesh.vertices = vertices;

        // Генерация треугольников
        int[] triangles = {
            0, 2, 1,
            1, 2, 3
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
    }
}