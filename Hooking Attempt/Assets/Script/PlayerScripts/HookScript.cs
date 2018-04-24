using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This codes main purpose is to move the hook of the player from a starting position
/// to a new position. It also does layer detection with the hook to see if you can 
/// hook the object or not.
/// 
/// FIXES NEEDED:
/// Make it so you can shoot the hook while moving
/// Change the hook so it moves with your mouse. (try rotating on the z-axis)
/// Check for layer detection using the hook.
/// Check the proximity of mathf.epsilon to see if their is a better comparision.
/// </summary>

public class HookScript : MonoBehaviour {

    public float lerpTime;
    public Vector3 transformBuffer;
    public float timeBuffer;

    private bool hookShot;
    private float hookShotTime;

    private float xHalfSize;
    private float yHalfSize;

    private Transform player;

    private Vector3 newPosition;
    //private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        
        Transform[] temp;
        temp = GetComponentsInParent<Transform>();

        foreach (Transform check in temp)
        {
            if (check.CompareTag("Player"))
                player = check.transform;
        }

        xHalfSize = player.localScale.x / 2.0f;
        yHalfSize = player.localScale.y / 2.0f;

        hookShot = false;
        hookShotTime = 0;

        newPosition = Vector2.zero;
	}
	
    // Update is called every frame
	void Update () {
        //hookMovement();

        if (Input.GetMouseButtonDown(0) || hookShot)
        {
            hookSetup();
            hookMovement();
        }

        mouseTrack();
	}

    //Checks the distance between the transform and the orginal position.
    private float checkPosition()
    {
        return Vector2.Distance(transform.position, newPosition);
    }

    // Meant to move the hook to where the mouse is pointing relative to the player
    private void mouseTrack()
    {
        /*Vector3 changePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse: " + changePosition);

        float clampX = player.position.x + xHalfSize;
        float clampY = player.position.y + yHalfSize;

        newPosition.x = Mathf.Clamp(changePosition.x, -clampX, clampX);
        newPosition.y = Mathf.Clamp(changePosition.y, -clampY, clampY);

        transform.localPosition = new Vector3(newPosition.x, newPosition.y, -5f);*/
    }

    //Setups the timer as well as the hooks position to be shot
    private void hookSetup()
    {
        //Only runs once well the hook is being shot
        if (!hookShot)
        {
            hookShot = true;
            hookShotTime = Time.time + timeBuffer;
            Vector3 rawPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition = new Vector3(rawPosition.x, rawPosition.y, 0.0f);
        }

        float resultTime = hookShotTime - Time.time;
        Debug.Log(resultTime);

        // Checks to see if the time is up for the distance the hook can do
        if (resultTime <= 0f)
        {
            hookShot = false;
            transform.localPosition = new Vector3(transformBuffer.x, 0.0f, transformBuffer.z);
            // Reset the position using lerp here
        }
    }

    //Moves hook for a certain period of time in a general direction.
    private void hookMovement()
    {
        // Makes the hook shoot for ten seconds before stopping and coming back to the player

        Debug.Log("Hooking");
        //Debug.Log(originalPosition);
        if (hookShot)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, lerpTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, newPosition);
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
