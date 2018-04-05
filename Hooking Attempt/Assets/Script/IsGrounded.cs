using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script can check for any object whether they are on the ground or not. 
/// Also gives access to the boolean grounded through a property
/// Still needs more work for edge checking. Ray cast may be required.
/// </summary>


public class IsGrounded : MonoBehaviour {

    private float xHalfSize;
    private float yHalfSize;

    private bool grounded;

    private LayerMask groundLayer;

    private Rigidbody2D rb2D;

    // This is a property that only lets you get the value of grounded
    public bool Grounded
    {
        get { return grounded; }
    }

	// Use this for initialization
	void Start () {
        xHalfSize = transform.localScale.x / 2.0f;
        yHalfSize = transform.localScale.y / 2.0f;

        groundLayer = LayerMask.GetMask("ground");

        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        grounded = isGround();
	}

    //Creates a rectanglur shape around the feet of the player in order to check if their is ground
    // If the rectangle shape over laps with another collider then it is grounded
    private bool isGround()
    {
        Vector2 firstArea = new Vector2(transform.position.x - xHalfSize, transform.position.y - yHalfSize);
        Vector2 secondArea = new Vector2(transform.position.x + xHalfSize, transform.position.y - yHalfSize);

        // Creates the overlap area that checks whether or not we are in contact
        // with the ground collider. Should check Unity API for more details.
        return Physics2D.OverlapArea(firstArea, secondArea, groundLayer);
    }
}
