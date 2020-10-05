using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int gridX = 5;
    private int gridY = 5;

    // Start is called before the first frame update
    void Start()
    {
        SetPosition(gridX, gridY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gridY++;
            SetPosition(gridX, gridY);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            gridY--;
            SetPosition(gridX, gridY);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gridX--;
            SetPosition(gridX, gridY);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            gridX++;
            SetPosition(gridX, gridY);
        }
    }

    void SetPosition(int gridX, int gridY)
    {
        Grid grid = GameObject.FindObjectOfType<Grid>();
        transform.position = grid.GridToWorldPosition(new Vector3(gridX, gridY, 0));
    }
}
