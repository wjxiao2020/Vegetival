using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupIntroTrigger : MonoBehaviour
{
    bool triggeredIntro = false;
    public GameObject introCanvas;
    // Start is called before the first frame update
    void Start()
    {
        triggeredIntro = false;
        introCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggeredIntro)
        {
            
            triggeredIntro = true;
            
            introCanvas.gameObject.SetActive(true);
            var script = introCanvas.GetComponent<PickupIntro>();
            script.InvokeIntro();
        }
    }
}
