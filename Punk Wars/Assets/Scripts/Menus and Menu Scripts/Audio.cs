using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    
    int Sound = 0;
    public AudioSource src;
    public AudioClip sfx1, sng1, sng2;

    public void Button()
    {
        src.clip = sfx1;
        src.Play();
    }

}

