using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    public AudioClip buttonSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickSound()
    {
        // Play the click sound effect
        print("play click sfx");
        AudioSource.PlayClipAtPoint(buttonSFX, Camera.main.transform.position, 1);
    }
}
