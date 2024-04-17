using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings : MonoBehaviour
{
    public static float mouseSensitivity = 100;
    public static float soundVolume = 1;
    public static float speedBoostCount = 0;

    public Slider mouseSensitivitySlider;
    public Slider soundVolumeSlider;

    public void UpdateMouseSensitivity()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
    }

    public void UpdateSoundVolume()
    {
        soundVolume = soundVolumeSlider.value;
    }

    public static void UpdateSpeedBoostCount(int numChanged)
    {
        speedBoostCount += numChanged;
    }
}
