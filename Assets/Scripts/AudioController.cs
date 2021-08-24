using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bg;
    public AudioClip win;
    public AudioClip lose;

    void Start()
    {
        GetComponent<AudioSource>().clip = bg;
        GetComponent<AudioSource>().Play();
    }

    public void PlaySoundWin(bool win_)
    {
        GetComponent<AudioSource>().loop = false;
        if (win_)
        {
            GetComponent<AudioSource>().clip = win;
        }
        else
        {
            GetComponent<AudioSource>().clip = lose; 
        }
        GetComponent<AudioSource>().Play();
    }

}
