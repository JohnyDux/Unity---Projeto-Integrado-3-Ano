using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public GameObject openDoorSprite;

    private void Start()
    {
        openDoorSprite.SetActive(false);
    }
    void OnMouseEnter()
    {
        openDoorSprite.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }

    void OnMouseExit()
    {
        openDoorSprite.SetActive(false);
    }
}
