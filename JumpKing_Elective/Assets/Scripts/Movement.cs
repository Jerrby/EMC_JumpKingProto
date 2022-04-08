using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;
    public float jumpHeight = 0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(isGrounded && jumpHeight == 0.0f)
        {
           rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
      
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y -1f), new Vector2(0.9f,0.4f),0f,groundMask);

        if (jumpHeight > 0)
        {
            rb.sharedMaterial = bounceMat;
        }
        else rb.sharedMaterial = normalMat;

        if (Input.GetKey("space") && canJump && isGrounded) 
        {
            jumpHeight += 0.1f;
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpHeight >= 20f && isGrounded )
        {
            float tempx = moveInput * moveSpeed;
            float tempy = jumpHeight;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", .2f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveSpeed * moveSpeed, jumpHeight);
                jumpHeight = 0;
            }
            canJump = true;
        }
    }

    void ResetJump()
    {
        canJump = false;
        jumpHeight = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, transform.position.y - 1f), new Vector2(0.9f, 0.2f));
    }
}
