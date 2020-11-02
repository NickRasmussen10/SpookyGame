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

    static public Player playerRef;
    static public SceneMngr mngrRef;
    protected Grid gridRef;
   

    protected bool dealKnockback;

    // Getters and Setters
    public int GridX { get { return gridX; } }
    public int GridY { get { return gridY; } }

    // Start is called before the first frame update
    void Start()
    {
        gridRef = FindObjectOfType<Grid>();
        playerRef = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    protected void Update()
    {
        // Check for damage differences
        switch (mngrRef.DifficultyLevel)
        {
            case 5:
                damageScale = 2;
                break;
            case 10:
                damageScale = 3;
                break;
            default:
                break;
        }

        // Check for damage
        if (playerRef.GridY == gridY && playerRef.GridX == gridX)
        {
            playerRef.TakeDamage(dealKnockback, damage * damageScale);
        }
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
