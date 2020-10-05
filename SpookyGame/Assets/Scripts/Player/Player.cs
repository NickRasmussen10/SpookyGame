using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int gridX = 5;
    private int gridY = 5;

    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        SetPosition(gridX, gridY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grid.VerifyPosition(gridX, gridY + 1))
            {
                gridY++;
                SetPosition(gridX, gridY);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (grid.VerifyPosition(gridX, gridY - 1))
            {
                gridY--;
                SetPosition(gridX, gridY);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (grid.VerifyPosition(gridX - 1, gridY))
            {
                gridX--;
                SetPosition(gridX, gridY);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (grid.VerifyPosition(gridX + 1, gridY))
            {
                gridX++;
                SetPosition(gridX, gridY);
            }
        }
    }

    void SetPosition(int gridX, int gridY)
    {
        transform.position = grid.GridToWorldPosition(new Vector3(gridX, gridY, 0));
    }
}
