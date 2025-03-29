using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7f;
    public float Gravity = 20.0f;
    public Transform playerCamera;
    CharacterController player;
    public Canvas levelCompleteCanvas;
    float verticalRotation = 0.0f;
    float verticalVelocity = 0.0f;
    bool levelComplete = false;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (levelComplete)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90.0f, 90.0f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        Vector3 desiredMoveDirection = forward * vertical + right * horizontal;
        Vector3 velocity = desiredMoveDirection * moveSpeed * Time.deltaTime;

        if (player.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce/ moveSpeed;
            }
        }

        if (!player.isGrounded)
        {
            verticalVelocity -= Gravity * Time.deltaTime;
        }

        velocity.y = verticalVelocity;

        player.Move(velocity);
    }

    public void OnLevelComplete()
    {
        levelComplete = true;
        levelCompleteCanvas.gameObject.SetActive(true);
        
    }
}