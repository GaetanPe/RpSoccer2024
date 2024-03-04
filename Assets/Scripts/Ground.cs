using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private int count = 2;
    [SerializeField] private float height = 25;
    [SerializeField] private float scale = 0.01f;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Mesh mesh = new Mesh();
        mesh.name = "My grid";

        Vector3[] vertices = new Vector3[(count + 1) * (count + 1)];
        int[] triangles = new int[(count) * (count) * 6];
        Vector2[] uv = new Vector2[vertices.Length];
        int index = 0;

        // Vertices, UV
        for (int y = 0; y < count + 1; y++)
        {
            for (int x = 0; x < count + 1; x++)
            {
                float noise = Mathf.PerlinNoise(x * scale, y * scale);


                vertices[index] = new Vector3(x, noise * height, y);
                uv[index] = new Vector2(x / (float)(count + 1), y / (float)(count + 1));

                index++;
            }
        }


        int ti = 0, vi = 0;

        // Triangles
        for (int y = 0; y < count; y++)
        {
            for (int x = 0; x < count; x++)
            {
                // Triangle 1
                triangles[ti] = vi;
                triangles[ti + 1] = vi + count + 1;
                triangles[ti + 2] = vi + 1;

                // Triangle 2
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + count + 1;
                triangles[ti + 5] = vi + count + 2;

                ti += 6;
                vi++;
            }
            vi++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
