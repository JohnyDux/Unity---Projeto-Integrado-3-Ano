using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAction : MonoBehaviour
{

    public float width = 10f;
    public float height = 5f;
    public GameObject pianoTile;
    float startDelay;
    public float delayValue;
    public float min = -5f;
    public float max = 10f;

    Transform freeposition;

    public Score score;
    public int currentAmountPieces;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    void Start()
    {
        startDelay = 2f;
    }

    void Update()
    {
        if(startDelay <= 0)
        {
            startDelay = delayValue;
        }

        if(startDelay > 0)
        {
            startDelay -= Time.deltaTime;
        }
        else if (score.scorePoints < 5)
        {
            delayValue = 0.5f;
        }
        else if (score.scorePoints < 10 && score.scorePoints > 5)
        {
            delayValue = 0.3f;
        }
        else if (score.scorePoints < 15 && score.scorePoints > 10)
        {
            delayValue = 0.1f;
        }

        if (score.timeLeft > 2 && startDelay <= 1 && currentAmountPieces < 3)
        {
             spawner();
        }
    }

    void spawner()
    {
        foreach (Transform child in transform)
            {
                GameObject piano = Instantiate(pianoTile, child.position, Quaternion.identity);
                pianoTile.GetComponent<TileAction>().fallingSpeed = Random.Range(15f, 35f);
                piano.transform.parent = child;
                currentAmountPieces++;
            }  
    }

    bool checkforempty()
    {
        foreach (Transform child in transform)
        {
            //verifica se não tem espaço vazios
            if (child.childCount > 0)
                {
                    return false;
                }
        }
        //verifica se tem espaços vazios
        return true;
    }
}
