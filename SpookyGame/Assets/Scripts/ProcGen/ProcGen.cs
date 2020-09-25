using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen : MonoBehaviour
{
    //temp grid stuff just to get the basics, this will change later
    [SerializeField] GameObject pref_gridObj;
    GameObject[,] grid;
    float cellWidth = 1;


    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[40, 20];
        for (int c = 0; c < grid.GetLength(0); c++)
        {
            for (int r = 0; r < grid.GetLength(1); r++)
            {
                grid[c, r] = Instantiate(pref_gridObj, new Vector3((c - (grid.GetLength(0) / 2)) * cellWidth, (r - (grid.GetLength(1) / 2)) * cellWidth, 0), Quaternion.identity);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int x = Random.Range(0, grid.GetLength(0));
            int y = Random.Range(0, grid.GetLength(1));
            int width = Random.Range(3, 10);
            int height = Random.Range(3, 10);
            GenerateRoom(x, y, width, height);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRoom(int pX = 0, int pY = 0, int pWidth = 1, int pHeight = 1)
    {
        for (int c = pX; c < pX + pWidth; c++)
        {
            for (int r = pY; r < pY + pHeight; r++)
            {
                if (c > grid.GetLength(0) - 1) continue;
                else if (c < 0) continue;
                else if (r > grid.GetLength(1) - 1) continue;
                else if (r < 0) continue;
                //grid[c, r].GetComponent<GridObj>().SetState(1);
            }
        }
    }
}
