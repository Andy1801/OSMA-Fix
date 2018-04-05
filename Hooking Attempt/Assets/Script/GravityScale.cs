using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script checks if the object is falling and if so increase
/// how effective gravity will be.
/// </summary>

public class GravityScale : MonoBehaviour {

    public float gravityNormal;
    public float gravityModifier;

    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb2D.velocity.y < 0.0)
            rb2D.gravityScale = gravityModifier;
        else
            rb2D.gravityScale = gravityNormal;
	}
}
