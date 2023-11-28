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

    }

    void spawner()
    {
        foreach(Transform child in transform)
        {
            GameObject piano = Instantiate(pianoTile, child.position, Quaternion.identity);
            piano.transform.parent = child;
        }
    }
}
