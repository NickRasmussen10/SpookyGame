using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : Enemy
{
    // Variables
    [SerializeField] private int sprintRadius;

    [SerializeField] private int gridXStart;
    [SerializeField] private int gridYStart;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        speed = 0.5f;
        dealKnockback = true;
    }

    // Update is called once per frame
    new void Update()
    {
        // CALL BASE UPDATE
        base.Update();


        // MUMMY CLASS UPDATE

        // Check for player to be adjacent to them within the sprint range
        if (Mathf.Abs(gridX - playerRef.GridX) < sprintRadius && Mathf.Abs(gridY - playerRef.GridY) < sprintRadius)
        {
            // If the player is on the same x as the mummy is, charge towards them on the X axis
            if (playerRef.GridX == gridX)
            {

            }
            // Else, if the player is on the same y, charge towards them
            else if (playerRef.GridY == gridY)
            {

            }
        }


        // DEBUG STUFF
        Debug.Log("Mummy Update");
    }

    private void Movement()
    {
        //something something it should always move towards the player when it starts and then it should stop if it hits a wall or hits the player and then return to it's starting location
    }
}
