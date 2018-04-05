using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The script focuses on both the players horizontal and vertical movement.
/// It performs checks for both of those sperate in order to better handle 
/// collision as well as fine tune the movement.
/// </summary>

public class PlayerMovement : MonoBehaviour {

    public float horizentalModifier;
    public float verticalModifier;

    public float horizontalDivider;

    private Vector2 newPosition;

    private Transform feet;
    private Rigidbody2D rb2D;

    private IsGrounded check;

	// Use this for initialization
	void Start () {
        check = GetComponent<IsGrounded>();

        feet = GetComponentInChildren<Transform>(); 
        rb2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once every frame
    private void Update()
    {
        VerticalMovement();
    }

    // FiixedUpdate is called every physics step
    private void FixedUpdate () {
        newPosition.x = HorizontalMovement();

        rb2D.position += newPosition;
	}

    // Takes the horizontal direction that player wants to move in with the 'A' and 'D' keys 
    private float HorizontalMovement()
    {
        // Gets value being either 0(key not pressed) or 1 (key pressed)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Uses the horizontalModifier to give it a greater value then 1. 
        // Time.deltatime to account for framerate jumps or drops
        float finalHorizontal = horizontal * horizentalModifier * Time.deltaTime;

        //When jumping your speed will be divided by horizontalDivider
        if (rb2D.velocity.y != 0.0)
            finalHorizontal /= horizontalDivider;

        return finalHorizontal;
    }

    // Is used to jump by pressing down the space button
    private void VerticalMovement()
    {
        // Checks for correct key press as well as if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && check.Grounded)
        {
            // Sets whatever velocity the player had before hand to zero 
            // to avoid any strange long jumps
            rb2D.velocity = Vector2.zero;

            // The force that is added is at the feet that is a empty object 
            // located under the player
            rb2D.AddForceAtPosition(Vector2.up * verticalModifier, feet.position);
        }
    }
}
