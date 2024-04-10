using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettings : MonoBehaviour
{
    public static float mouseSensitivity;

    public Slider mouseSensitivitySlider;

    public void UpdateMouseSensitivity()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
    }
}
