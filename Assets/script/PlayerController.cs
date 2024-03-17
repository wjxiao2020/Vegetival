using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Vector3 input, moveDirection;
    public float speed = 5.0f;
    private int speedBoostCount = 0;
    public Text speedBoostCountText;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 10;
    private bool isSpeedBoostActive = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        input *= speed;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateSpeedBoost();
        }

        if (controller.isGrounded)
        {
            moveDirection = input;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void AddSpeedBoost()
    {
        speedBoostCount++;
        UpdateSpeedBoostCountUI();
    }

    private void ActivateSpeedBoost()
    {
        if (speedBoostCount > 0 && !isSpeedBoostActive)
        {
            speedBoostCount--;
            UpdateSpeedBoostCountUI();
            StartCoroutine(ApplySpeedBoost());
        }
    }

    private IEnumerator ApplySpeedBoost()
    {
        isSpeedBoostActive = true;
        float originalSpeed = speed;
        speed *= 2;
        yield return new WaitForSeconds(3);
        speed = originalSpeed;
        isSpeedBoostActive = false;
    }

    private void UpdateSpeedBoostCountUI()
    {
        speedBoostCountText.text = "Speed Boosts: " + speedBoostCount.ToString();
    }
}
