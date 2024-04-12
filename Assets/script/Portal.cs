using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public GameObject beam;
    public GameObject particle;
    public float beamGrowSpeed = 0.1f;
    bool initiate = false;
    public GameObject interactText;
    bool isInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        beam.SetActive(false); 
        particle.SetActive(false);
        initiate = false;
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 store = beam.transform.localScale;

        if (initiate)
        {
            if (store.y < 260)
            {
                beam.transform.localScale = new Vector3(store.x, store.y + beamGrowSpeed, store.z);
            }

            if (isInRange && Input.GetKeyDown(KeyCode.E))
            {
                if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount + 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }


    }

    public void ReadyToTeleport()
    {
        beam.SetActive(true);
        particle.SetActive(true);
        initiate =true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && initiate)
        {
            interactText.gameObject.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
            isInRange = false;
        }
    }
}
