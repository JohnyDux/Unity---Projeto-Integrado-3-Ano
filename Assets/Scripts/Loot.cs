using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    MazeScore maze;
    private void Start()
    {
        string objectName = "Canvas";

        GameObject foundObject = GameObject.Find(objectName);

        maze = foundObject.GetComponent<MazeScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            maze.score = maze.score + 1;
            Destroy(gameObject);
        } 
    }
}
