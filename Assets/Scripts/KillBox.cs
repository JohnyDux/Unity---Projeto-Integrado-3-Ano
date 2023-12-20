using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillBox : MonoBehaviour
{
    public MazePlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            player.lifes = 0;
        }
    }
}
