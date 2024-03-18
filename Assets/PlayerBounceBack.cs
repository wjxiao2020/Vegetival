using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceBack : MonoBehaviour
{
    public static float backUpTimeLeft;
    public static float backUpScale = 10;
    public static Vector3 backUpDirection;
    CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        backUpTimeLeft = 0;
        backUpDirection = Vector3.zero;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (backUpTimeLeft > 0)
        {
            playerController.Move(backUpDirection.normalized * backUpScale * Time.deltaTime);
            backUpDirection = backUpDirection * 0.7f;
            backUpTimeLeft -= Time.deltaTime;
        }
        else
        {
            backUpTimeLeft = 0;
        }
    }
}
