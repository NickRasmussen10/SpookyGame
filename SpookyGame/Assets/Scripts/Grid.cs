using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Grid : MonoBehaviour
{
    public int rows;
    public int cols;
    public float tileSize = 1;

    [SerializeField] GameObject pref_GridTile;

    public GameObject[,] grid;
    int[,] grid_modules;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[cols, rows];
        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                GameObject tile = Instantiate(pref_GridTile, new Vector3(c * tileSize - (tileSize * cols / 2), r * tileSize - (tileSize * rows / 2), 0), Quaternion.identity);
                grid[c, r] = tile;
                tile.transform.parent = gameObject.transform;
            }
        }

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //level generation stuff oh boy here we go
    void GenerateLevel()
    {
        //TODO: change this to a 3D array
        string[] module0 = File.ReadAllLines("Assets/Resources/modules_type0.txt");
        string[] module1 = File.ReadAllLines("Assets/Resources/modules_type1.txt");
        string[] module2 = File.ReadAllLines("Assets/Resources/modules_type2.txt");

        grid_modules = new int[4, 4];
        for(int r = 0; r < grid_modules.GetLength(0); r++)
        {
            for(int c = 0; c < grid_modules.GetLength(1); c++)
            {
                int moduleType = Random.Range(0, 3);
                grid_modules[c, r] = moduleType;

                switch (moduleType)
                {
                    case 0:
                        FillGridWithModule(c * 10, r * 8, module0);
                        break;
                    case 1:
                        FillGridWithModule(c * 10, r * 8, module1);
                        break;
                    case 2:
                        FillGridWithModule(c * 10, r * 8, module2);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void FillGridWithModule(int pX, int pY, string[] module)
    {
        for(int r = 0; r < module.Length; r++)
        {
            for(int c = 0; c < module[r].Length; c++)
            {
                char tileType = module[r][c];
                switch (tileType)
                {
                    case '#':
                        grid[pX + c, pY + r].GetComponent<GridObj>().SetState(1);
                        break;
                    case '.':
                        grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
