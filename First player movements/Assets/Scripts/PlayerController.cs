using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed = 7.0f;
    public float Gravity = 9.8f;
    float velocity = 0;

    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((Vector3.forward * horizontal + Vector3.right * -vertical)* Time.deltaTime);

        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else { 
            velocity -= Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity, 0));
        }
    }
}
