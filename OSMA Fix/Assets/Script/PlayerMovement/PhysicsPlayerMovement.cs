using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlayerMovement : MonoBehaviour {

    public float horizontalModifier;
    public float verticalModifier;

    private Vector2 newPosition;

    private Transform feet;

    private IsGrounded groundCheck;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {

        newPosition = Vector2.zero;

        feet = GetComponentInChildren<Transform>();

        groundCheck = GetComponent<IsGrounded>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        newPosition.x = HorizontalMovement();

        VerticalMovement();

        rb2D.position += newPosition;
	}

    private float HorizontalMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        return horizontal * horizontalModifier * Time.deltaTime;
    }

    private void VerticalMovement()
    {
        if (Input.GetButtonDown("Vertical") && groundCheck.Grounded)
        {
            rb2D.AddForceAtPosition(new Vector2(0.0f, 1 * verticalModifier), feet.position);
        }
    }

    private Vector2 AddToTransform()
    {
        Vector3 holdPosition = newPosition;

        return transform.position += holdPosition;
    }
}
