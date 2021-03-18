using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public float movementSpeed = 6f;
    public float jumpForce = 10f;

    private float horizontalMovement;
    private Rigidbody2D body;
    private BoxCollider2D collider;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalMovement * movementSpeed, body.velocity.y);
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = Vector2.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Color rayColor;
        float heightOffset = 0.05f;

        RaycastHit2D hit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + heightOffset, platformLayerMask);

        if(hit.collider != null) 
            rayColor = Color.green;
        else 
            rayColor = Color.red;

        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + heightOffset), rayColor);
        return hit.collider != null;
    }
}
