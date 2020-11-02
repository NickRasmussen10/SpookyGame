using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    private int gridX = 5;
    private int gridY = 5;

    private int PrevSpaceX;
    private int PrevSpaceY;

    private int health = 5;

    Grid grid;

    // Getters and Setters
    public int GridX { get { return gridX; } }
    public int GridY { get { return gridY; } }
    public int Health { get { return health; } set { health = value; } }

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        SetPosition(gridX, gridY);

        PrevSpaceX = gridX;
        PrevSpaceY = gridY;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grid.VerifyPosition(gridX, gridY + 1))
            {
                PrevSpaceX = gridX;
                PrevSpaceY = gridY;
                gridY++;
                SetPosition(gridX, gridY);
            }
        }
        /*else*/ if (Input.GetKeyDown(KeyCode.S))
        {
            if (grid.VerifyPosition(gridX, gridY - 1))
            {
                PrevSpaceX = gridX;
                PrevSpaceY = gridY;
                gridY--;
                SetPosition(gridX, gridY);
            }
        }
        /*else*/ if (Input.GetKeyDown(KeyCode.A))
        {
            if (grid.VerifyPosition(gridX - 1, gridY))
            {
                PrevSpaceX = gridX;
                PrevSpaceY = gridY;
                gridX--;
                SetPosition(gridX, gridY);
            }
        }
        /*else*/ if (Input.GetKeyDown(KeyCode.D))
        {
            if (grid.VerifyPosition(gridX + 1, gridY))
            {
                PrevSpaceX = gridX;
                PrevSpaceY = gridY;
                gridX++;
                SetPosition(gridX, gridY);
            }
        }
    }

    void SetPosition(int gridX, int gridY)
    {
        transform.position = grid.GridToWorldPosition(new Vector3(gridX, gridY, 0));
    }

    public void TakeDamage(bool knockback, int amt)
    {
        if (knockback)
        {
            gridX = PrevSpaceX;
            gridY = PrevSpaceY;
            SetPosition(gridX, gridY);
        }

        health -= amt;
    }
}
