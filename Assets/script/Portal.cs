using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject beam;
    public GameObject particle;
    public float beamGrowSpeed = 0.1f;
    bool initiate = false;
    // Start is called before the first frame update
    void Start()
    {
        beam.SetActive(false); 
        particle.SetActive(false);
        initiate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (initiate)
        {
            Vector3 store = beam.transform.localScale;
            beam.transform.localScale = new Vector3(store.x, store.y + beamGrowSpeed, store.z);
        }
    }

    public void ReadyToTeleport()
    {
        beam.SetActive(true);
        particle.SetActive(true);
        initiate =true;
    }
}
