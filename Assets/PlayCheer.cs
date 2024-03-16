using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCheer : MonoBehaviour
{
    static int startSecond = 5;
    static AudioSource cheer;

    private void Start()
    {
        cheer = gameObject.GetComponent<AudioSource>();
    }

    public static void Play(float volume) {
        cheer.Stop();
        cheer.time = startSecond;
        cheer.volume = volume;
        cheer.Play();
    }

    public static bool isPlaying()
    {
        return cheer.isPlaying;
    }
}
