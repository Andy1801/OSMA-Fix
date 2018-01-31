using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayerMovement : MonoBehaviour {

    public float horizontalModifier;
    public float verticalModifier;
    public float verticalLerpModifier;

    private float jump;

    private Vector2 newPosition;

    private IsGrounded groundCheck;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {

        jump = 0.0f;
        
        newPosition = Vector2.zero;

        groundCheck = GetComponent<IsGrounded>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(verticalModifier * Time.fixedDeltaTime);

        newPosition.x = HorizontalMovement();
        newPosition.y = VerticalMovement();

        transform.Translate(newPosition);
	}

    private float HorizontalMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        return horizontal * horizontalModifier * Time.deltaTime;
    }

    private float VerticalMovement()
    {
        if (Input.GetButtonDown("Vertical") && groundCheck.Grounded)
            jump = verticalModifier * Time.deltaTime;
        else if (groundCheck.Grounded)
            jump = 0.0f;

        jump = Mathf.Lerp(0.0f, jump, verticalLerpModifier);

        return jump;
    }
}
