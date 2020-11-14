using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    protected int gridX;
    protected int gridY;

    protected int damage = 1; // amount of damage this enemy's attacks deal
    protected int damageScale = 1;

    protected int amntIframe = 1; // 2 seconds cooldown
    protected float iFrameCounter = 0; // counts the seconds until damage is dealt again

    protected float speed; // represents the number of seconds until the enemy moves again

    static public Player playerRef;
    static public SceneMngr mngrRef;
    protected Grid gridRef;
   

    protected bool dealKnockback;

    // Getters and Setters
    public int GridX { get { return gridX; } }
    public int GridY { get { return gridY; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gridRef = FindObjectOfType<Grid>();
        playerRef = FindObjectOfType<Player>();
        mngrRef = FindObjectOfType<SceneMngr>();

        iFrameCounter = amntIframe; // set the iframecounter so you can take damage from the start of the game
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Check for damage differences
        /*switch (mngrRef.DifficultyLevel)
        {
            case 5:
                damageScale = 2;
                break;
            case 10:
                damageScale = 3;
                break;
            default:
                break;
        }*/

        // Check for damage and increment frame counter
        if (iFrameCounter >= amntIframe)
        {
            if (playerRef.GridY == gridY && playerRef.GridX == gridX)
            {
                playerRef.TakeDamage(dealKnockback, damage * damageScale);
                iFrameCounter = 0;
            }
        }
        else
        {
            // Increase iFrameCounter every frame
            iFrameCounter += Time.deltaTime;
        }

        // DEBUG STUFF
        Debug.Log("Update from Enemy");
        Debug.Log(iFrameCounter);
    }

    public void setGridLoc(int _gridX, int _gridY)
    {
        gridX = _gridX;
        gridY = _gridY;
    }

    protected void SetPosition(int gridX, int gridY)
    {
        transform.position = gridRef.GridToWorldPosition(new Vector3(gridX, gridY, 0));
    }
}
