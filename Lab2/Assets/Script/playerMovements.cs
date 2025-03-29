using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float gravity = 20f;

    private bool isGrounded;
    private CharacterController player;
    private Vector3 playerVelocity;
    private float endOfPlayground = 270f;

    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = player.isGrounded;

        if (transform.position.z <= endOfPlayground)
        {
            player.Move(new Vector3(0f, 0f, moveSpeed) * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpForce;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;

        if (!isGrounded)
        {
            playerVelocity.y -= gravity * Time.deltaTime;
        }

        movement.y = playerVelocity.y;

        player.Move(movement * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1f, 1f), transform.position.y, transform.position.z);
    }
}