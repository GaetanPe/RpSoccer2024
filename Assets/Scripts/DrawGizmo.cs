using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public float maxRadius;
    public int maxPoints;
    public Vector2 regionSize;
    public int rejectionSamples;
    [SerializeField] public ObjectProba[] prefabs;

    List<Transform> Objects;
    List<Vector2> points;
    List<GameObject> objects;

    void Start()
    {
        objects = new List<GameObject>();
        GeneratePoints();
        InstantiatePoints();

    }

    void GeneratePoints()
    {
        points = PoissonDiscSampling.GeneratePoints(prefabs, regionSize, rejectionSamples, maxPoints, maxRadius);
    }

    void InstantiatePoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 pos = transform.position + new Vector3(points[i].x, 0.01f, points[i].y);
            GameObject go = Instantiate(PickRandomObject(), pos, Quaternion.identity);
            GameObject go1 = Instantiate(go, -pos, go.transform.rotation);
            go1.transform.Rotate(new Vector3(0, 180, 0));

        }
    }

    GameObject PickRandomObject()
    {
        float totalWeight = 0;
        for (int i = 0; i < prefabs.Length; i++)
        {
            totalWeight += prefabs[i].proba;
        }

        float rand = Random.value;

        for (int i = 0; i < prefabs.Length; i++)
        {
            if (rand <= prefabs[i].proba / totalWeight)
            {
                return prefabs[i].go;
            }
            else
            {
                rand -= prefabs[i].proba / totalWeight;
            }
        }
        return prefabs[prefabs.Length].go;
    }
}

[System.Serializable]
public class ObjectProba
{
    [SerializeField] public GameObject go;
    [SerializeField][Range(0f, 1f)] public float proba;
}