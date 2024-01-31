using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject normalWrenchSprite;
    public GameObject rotateWrenchSprite;

    private void Start()
    {
        normalWrenchSprite.SetActive(true);
        rotateWrenchSprite.SetActive(false);
    }
    void OnMouseEnter()
    {
        normalWrenchSprite.SetActive(false);
        rotateWrenchSprite.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }

    void OnMouseExit()
    {
        normalWrenchSprite.SetActive(true);
        rotateWrenchSprite.SetActive(false);
    }
}
