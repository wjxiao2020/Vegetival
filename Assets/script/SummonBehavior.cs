using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBehavior : MonoBehaviour
{
    private void OnDestroy()
    {
        var boss = GameObject.FindAnyObjectByType<LettuceLord>();
        var script = boss.GetComponent<LettuceLord>();
        script.BossDie();
    }
}
