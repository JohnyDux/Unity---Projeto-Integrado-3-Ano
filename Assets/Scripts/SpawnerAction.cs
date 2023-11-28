using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAction : MonoBehaviour
{

    public float width = 10f;
    public float height = 5f;
    public GameObject pianoTile;

    void Start()
    {
        spawner();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    void Update()
    {
        if (checkforempty())
        {
            spawner();
        }
    }

    void spawner()
    {
        foreach(Transform child in transform)
        {
            GameObject piano = Instantiate(pianoTile, child.position, Quaternion.identity);
            piano.transform.parent = child;
        }
    }

    bool checkforempty()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
