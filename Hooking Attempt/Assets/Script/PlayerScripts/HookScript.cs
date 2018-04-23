using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This codes main purpose is to move the hook of the player from a starting position
/// to a new position. It also does layer detection with the hook to see if you can hook or not.
/// 
/// FIXES NEEDED:
/// Make it so you can shoot the hook while moving
/// Change the hook so it moves with your mouse.
/// Check for layer detection using the hook.
/// Check the proximity of mathf.epsilon to see if their is a better comparision.
/// </summary>

public class HookScript : MonoBehaviour {

    public float lerpTime;
    public Vector3 transformBuffer;

    private bool hookShot;

    private Transform player;

    private Vector3 newPosition;
    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        
        Transform[] temp;
        temp = GetComponentsInParent<Transform>();

        foreach (Transform check in temp)
        {
            if (check.CompareTag("Player"))
                player = check.transform;
        }

        Debug.Log(player.gameObject.name);

        hookShot = false;

        newPosition = Vector2.zero;
        originalPosition = player.position + transformBuffer;
	}
	
    // Update is called every frame
	void Update () {
        //hookMovement();

        hookTimeMovement();

        originalPosition = player.position + transformBuffer;
	}

    //Checks the distance between the transform and the orginal position.
    private float checkPosition()
    {
        return Vector2.Distance(transform.position, newPosition);
    }

    //Moves hook for a certain period of time in a general direction.
    private void hookTimeMovement()
    {
        if(Input.GetMouseButtonDown(0))
          Debug.Log("Clicked mouse button");
    }





    //Moves the hook from originalPosition to newPosition and then resets
    /*private void hookMovement()
    {
        //Debug.Log(Input.GetMouseButton(0));


        //Checks if the left mouse button has been clicked. Can only be done once
        // before the hook resets
        if (Input.GetMouseButtonDown(0) && !hookShot)
        {
            Debug.Log("Mouse has been clicked");
            Vector2 rawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition = new Vector3(rawPosition.x, rawPosition.y, 0.0f);

            hookShot = true;
        }

        // if the distance between the two vectors is less then 1.6x10^-45 then
        // reset and you can shoot again.
        // else continue moving the hook towards the new position.
        if (checkPosition() < 0.1 && hookShot)
        {
            Debug.Log("Inside check if");
            transform.position = originalPosition;
            hookShot = false;
        }
        else if(hookShot)
        {
            Debug.Log("Inside lerping else");
            Vector2 lerpPosition = Vector2.Lerp(transform.position, newPosition, lerpTime);
            transform.position = lerpPosition;
        }
    }*/
}
