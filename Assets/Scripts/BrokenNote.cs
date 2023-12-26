using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenNote : MonoBehaviour
{
    public ParticleSystem brokenMusicNote;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TileAction tile = collision.GetComponent<TileAction>();
        if (!tile.isHit && collision.CompareTag("piano"))
        {
            StartParticleSystem();
        }
    }

    void StartParticleSystem()
    {
        if (brokenMusicNote != null && !brokenMusicNote.isPlaying)
        {
            brokenMusicNote.Play();
        }
    }

    void StopParticleSystem()
    {
        if (brokenMusicNote != null && brokenMusicNote.isPlaying)
        {
            brokenMusicNote.Stop();
        }
    }
}
