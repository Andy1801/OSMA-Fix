using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts focuses on moving a particular object
/// based on the position of the mouse and relative
/// only to the position of the main player.
/// 
/// FIXES NEEDED:
/// Check if you can make the movement have a smoother rotation around the player.
/// 
/// </summary>

public class MouseTracking : MonoBehaviour {

    private float xHalfSize;
    private float yHalfSize;

    private Transform player;

    private Vector3 newPosition;

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
    void Start () {
        xHalfSize = player.localScale.x / 2.0f;
        yHalfSize = player.localScale.y / 2.0f;

        newPosition = Vector2.zero;
    }

    // Meant to move the object to where the mouse is pointing relative to the player
    public void mouseTrack()
    {
        Vector3 changePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Finds the clamp on X-axis based on the players position and half of their X-localscale.
        float clampX = player.position.x + xHalfSize;
        float negClampX = player.position.x - xHalfSize;

        // Finds the clamp on Y-axis based on the players position and half of their Y-Localscale.
        float clampY = player.position.y + yHalfSize;

        ///REMEMBER: By turning this on the hook will not move around while inside the bottom of the player
        float negClampY = player.position.y - yHalfSize;

        // Clamps the mouses position to that of the X clamp and Y clamps found above
        newPosition.x = Mathf.Clamp(changePosition.x, negClampX, clampX);
        newPosition.y = Mathf.Clamp(changePosition.y, player.position.y, clampY);

        // Checks for if the mouse is inside the player
        bool checkX = (newPosition.x < clampX) && (newPosition.x > negClampX);
        bool checkY = (newPosition.y < clampY) && (newPosition.y > negClampY);

        // if the mouse is inside the player then do not move the hook.
        if (!checkX || !checkY)
            transform.position = new Vector3(newPosition.x, newPosition.y, -5f);
    }
}
