using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings : MonoBehaviour
{
    public static float mouseSensitivity = 100;
    public static float soundVolume;

    public Slider mouseSensitivitySlider;
    public Slider soundVolumeSlider;

    private void Start()
    {
        UpdateMouseSensitivity();
        UpdateSoundVolume();
    }

    public void UpdateMouseSensitivity()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
    }

    public void UpdateSoundVolume()
    {
        soundVolume = soundVolumeSlider.value;
    }
}
