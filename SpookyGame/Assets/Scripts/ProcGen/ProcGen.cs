using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProcGen : MonoBehaviour
{
    //temp grid stuff just to get the basics, this will change later
    [SerializeField] GameObject pref_gridObj;
    GameObject[,] grid;
    float cellWidth = 1;
    
    string[] modules;

    // Start is called before the first frame update
    void Start()
    {
        modules = File.ReadAllLines("Assets/Resources/modules.txt");
        grid = new GameObject[8 * 5, 8 * 3]; //multiplied by 8 because ProcGen modules are 8x8
        for (int c = 0; c < grid.GetLength(0); c++)
        {
            for (int r = 0; r < grid.GetLength(1); r++)
            {
                grid[c, r] = Instantiate(pref_gridObj, new Vector3((c - (grid.GetLength(0) / 2)) * cellWidth, (r - (grid.GetLength(1) / 2)) * cellWidth, 0), Quaternion.identity);
            }
        }

        for(int c = 0; c < grid.GetLength(0); c += 8)
        {
            for(int r = 0; r < grid.GetLength(1); r += 8)
            {
                GenerateRoom(c, r, Random.Range(0,12));
            }
        }
    }
     
    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRoom(int pX = 0, int pY = 0, int module = 0)
    {
        string[] room = new string[8];
        for(int s = 0; s < room.Length; s++)
        {
            room[s] = modules[(module*8) + s];
        }

        for(int c = 0; c < 8; c++)
        {
            for(int r = 0; r < 8; r++)
            {
                char curr = room[r][c];
                if (curr == '#') grid[pX + c, pY + r].GetComponent<GridObj>().SetState(1);
                else if (curr == '.') grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
            }
        }
    }
}
