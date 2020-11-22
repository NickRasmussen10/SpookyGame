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

    bool controlsActive = true;

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
        if (controlsActive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (grid.VerifyPosition(gridX, gridY + 1))
                {
                    PrevSpaceX = gridX;
                    PrevSpaceY = gridY;
                    gridY++;
                    StartCoroutine(MoveTo(gridX, gridY));
                }
            }
            /*else*/
            if (Input.GetKey(KeyCode.S))
            {
                if (grid.VerifyPosition(gridX, gridY - 1))
                {
                    PrevSpaceX = gridX;
                    PrevSpaceY = gridY;
                    gridY--;
                    StartCoroutine(MoveTo(gridX, gridY));
                }
            }
            /*else*/
            if (Input.GetKey(KeyCode.A))
            {
                if (grid.VerifyPosition(gridX - 1, gridY))
                {
                    PrevSpaceX = gridX;
                    PrevSpaceY = gridY;
                    gridX--;
                    StartCoroutine(MoveTo(gridX, gridY));

                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            /*else*/
            if (Input.GetKey(KeyCode.D))
            {
                if (grid.VerifyPosition(gridX + 1, gridY))
                {
                    PrevSpaceX = gridX;
                    PrevSpaceY = gridY;
                    gridX++;
                    StartCoroutine(MoveTo(gridX, gridY));

                    GetComponent<SpriteRenderer>().flipX = false;
                }
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

    public IEnumerator MoveTo(int gridX, int gridY)
    {
        controlsActive = false;
        float lerpVal = 0.0f;
        float x = 0.0f;
        float y = 0.0f;
        Vector3 start = transform.position;
        Vector3 destination = grid.GridToWorldPosition(new Vector3(gridX,gridY));

        while (lerpVal < 1.0f)
        {
            x = Mathf.Lerp(start.x, destination.x, lerpVal);
            y = Mathf.Lerp(start.y, destination.y, lerpVal);
            transform.position = new Vector3(x, y);

            lerpVal += 0.1f;
            yield return null;
        }
        controlsActive = true;
    }
}
