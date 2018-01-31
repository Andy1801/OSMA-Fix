using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour {

    private bool grounded;

    private float xHalfSize;
    private float yHalfSize;

    private LayerMask groundLayer;

    public bool Grounded
    {
        get { return grounded;}
    }

	// Use this for initialization
	void Start () {
        xHalfSize = transform.localScale.x / 2;
        yHalfSize = transform.localScale.y / 2;

        groundLayer = LayerMask.GetMask("Ground");
	}
	
	// Update is called once per frame
	void Update () {
        grounded = isGrounded();
	}

    private bool isGrounded()
    {
        Vector2 overLapA = new Vector2(transform.position.x - xHalfSize, transform.position.y - yHalfSize);
        Vector2 overLapB = new Vector2(transform.position.x + xHalfSize, transform.position.y - yHalfSize);

        return Physics2D.OverlapArea(overLapA, overLapB, groundLayer);
    }

}
