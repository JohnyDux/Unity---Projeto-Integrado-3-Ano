using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Button : MonoBehaviour
{
    public GameObject openCabinetSprite;

    private void Start()
    {
        openCabinetSprite.SetActive(false);
    }
    void OnMouseEnter()
    {
        openCabinetSprite.SetActive(true);
    }

    void OnMouseExit()
    {
        openCabinetSprite.SetActive(false);
    }
}
