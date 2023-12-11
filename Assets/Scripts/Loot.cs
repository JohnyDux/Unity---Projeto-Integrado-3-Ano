using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public MazeScore maze;

    private void OnTriggerEnter(Collider other)
    {
        maze.score = +1;
        Destroy(gameObject);
    }
}
