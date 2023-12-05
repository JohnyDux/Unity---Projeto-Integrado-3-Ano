using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerCheckers : MonoBehaviour
{
    public int numberOfMisses;
    public Animator ViolinistAnimator;
    private void Start()
    {
        numberOfMisses = 0;
    }

    void Update()
    {
        if (numberOfMisses > 20)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        TileAction tile = collision.GetComponent<TileAction>();
        if (!tile.isHit)
        {
            numberOfMisses++;
            ViolinistAnimator.SetBool("IsPlaying", false);
        } 
        Destroy(collision.gameObject);
    }
}
