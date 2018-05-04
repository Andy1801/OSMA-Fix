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
/// Check for layer detection using the hook.
/// Check the proximity of mathf.epsilon to see if their is a better comparision.
/// </summary>

public class HookScript : MonoBehaviour {

    public float xChange;
    public float yChange;

    public float lerpTime;
    public Vector3 transformBuffer;
    public float timeBuffer;

    private float xDifference;
    private float yDifference;

    private MouseTracking hookTracking;
    private bool hookShot;
    private float hookShotTime;

    private Transform player;

    private void Awake()
    {
        Transform[] temp;
        temp = GetComponentsInParent<Transform>();

        foreach (Transform check in temp)
        {
            if (check.CompareTag("Player"))
                player = check.transform;
        }
    }

    // Use this for initialization
    void Start ()
    {
        hookShot = false;
        hookShotTime = 0;

        hookTracking = GetComponent<MouseTracking>();
	}
	
    // Update is called every frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            hookSetup();
        }
        else if (hookShot)
        {
            clockDown();
            hookMovement();
        }
        else
            hookTracking.mouseTrack();
	}

    //Setups the the amount of time that the hook will travel for.
    private void hookSetup()
    {
        //Only runs once well the hook is being shot
        hookShot = true;
        xDifference = transform.position.x / xChange;
        yDifference = transform.position.y / yChange;
        hookShotTime = Time.time + timeBuffer;
    }

    //Counts the seconds until the hook stops moving and returns
    private void clockDown()
    {
        float resultTime = hookShotTime - Time.time;

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
       


        transform.position = new Vector3(transform.position.x + xDifference, transform.position.x + yDifference, transform.position.z);
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
