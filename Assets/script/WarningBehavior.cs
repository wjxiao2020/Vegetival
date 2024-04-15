using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WarningBehavior : MonoBehaviour
{
    Material warningMaterial;
    float startOpacity;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer warningMesh = GetComponent<MeshRenderer>();
        warningMaterial = warningMesh.material;
        //startOpacity = warningMaterial.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        warningMaterial.color = new Color(warningMaterial.color.r, warningMaterial.color.g, warningMaterial.color.b, Mathf.PingPong(Time.time, 0.6f));
    }
}
