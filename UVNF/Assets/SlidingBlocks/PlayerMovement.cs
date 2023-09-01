using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask iceLayer; // Layer mask for ice tiles

    Vector2 targetPosition;
    bool isMoving = false;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 direction = new( horizontalInput, verticalInput);
            direction = direction.normalized;

            if (Mathf.Abs(direction[0]) > 0.707f) {
                Vector2 v = new(Mathf.Sign(horizontalInput), 0);
                Move(v);
            }
            else if (Mathf.Abs(direction[1]) > 0.707f) {
                Vector2 v = new(0, Mathf.Sign(verticalInput));
                Move(v);
            }

        }
    }

     void Move(Vector2 direction)
    {
        isMoving = true;
        rb.velocity = direction.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) {
            isMoving = false;
        }
        
        if (collision.gameObject.CompareTag("Goal")) {
            Debug.Log("goal reached");
            Destroy(gameObject);
        }
    }

    
}
