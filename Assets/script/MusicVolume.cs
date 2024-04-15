using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{
    AudioSource music;
    float originalVolume;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        originalVolume = music.volume;
        UpdateVolume();
        music.ignoreListenerPause = true;
    }

    public void UpdateVolume()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1;
        music.volume = UserSettings.soundVolume * originalVolume;
        Time.timeScale = originalTimeScale;
    }
}
