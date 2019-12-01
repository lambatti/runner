using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=0.2f;
    public float jumpForce = 1;
    public float rayLength = 1;

    public bool grounded;

    public LayerMask ground;

    private Rigidbody2D rb;

    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // FixedUpdate is called every fixed frame-rate frame, use it when using Rigidbody
    // 50fps
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // Update is called once per frame
    // Real FPS
    void Update()
    {
        grounded = isGrounded();

        if (Input.GetButton("Jump") && grounded)
            rb.velocity = new Vector2(0, jumpForce);
    }

    bool isGrounded()
    {
        Vector3 boxPos = transform.position + new Vector3(bc.offset.x, bc.offset.y, 0);
        Vector2 posL = boxPos - new Vector3(bc.size.x / 2, 0);
        Vector2 posR = boxPos + new Vector3(bc.size.x / 2, 0);
        Vector2 direction = Vector2.down;

        RaycastHit2D hitL = Physics2D.Raycast(posL, direction, rayLength, ground);
        RaycastHit2D hitR = Physics2D.Raycast(posR, direction, rayLength, ground);

        return hitL.collider != null || hitR.collider != null;
    }
}