using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAction : MonoBehaviour
{

    public float width = 10f;
    public float height = 5f;
    public GameObject pianoTile;
    public float startDelay = 0.5f;
    public float min = -5f;
    public float max = 10f;

    Transform freeposition;

    public Score score;

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
        if (checkforempty() && score.timeLeft > 0)
        {
            spawner();
        }
    }

    void spawnuntil()
    {
        Transform position = freeposition;
        float rand = Random.Range(min, max);
        Vector3 offset = new Vector3(rand, 0, 0);

        if (position)
        {
            GameObject piano = Instantiate(pianoTile, position.transform.position + offset, Quaternion.identity);
            piano.transform.parent = position;
        }
        if (freeposition)
        {
            Invoke("spawnuntill", startDelay);
        }
    }

    void spawner()
    {
        startDelay -= Time.deltaTime;

        if (startDelay <= 0)
        {
            foreach (Transform child in transform)
            {
                GameObject piano = Instantiate(pianoTile, child.position, Quaternion.identity);
                piano.transform.parent = child;
            }
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
