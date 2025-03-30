using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = 20f;
    private bool isGrounded;
    private Vector2 velocity;
    private SpriteRenderer spriteRenderer;
    public GameManager logic;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameManager>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        velocity.x = horizontalInput * movementSpeed;
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        if (Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        float minX = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 0.5f;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Max(clampedPosition.x, minX);
        transform.position = clampedPosition;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);
    }

    void Jump()
    {
        velocity.y = jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0)
        {
            isGrounded = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            velocity.y = 0f;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 playerPosition = rb.position;
            Vector2 direction = contactPoint - playerPosition;

            if (Mathf.Abs(direction.y) < 0.5f * Mathf.Abs(direction.x))
            {
                GetComponent<DeathAnimation>().enabled = true;
                if (GameManager.Instance != null)
                {
                    logic.GameOver();
                    logic.RestartLevel(3f);
                }
                Destroy(gameObject, 3f);
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal.y > 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    bool DotTest(Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }

    bool Raycast(Vector2 direction)
    {
        LayerMask layerMask = LayerMask.GetMask("Default");

        if (rb.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rb.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rb;
    }
}
