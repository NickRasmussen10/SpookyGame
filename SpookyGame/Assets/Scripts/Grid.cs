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
    [SerializeField] GameObject pref_Candy;
    [SerializeField] GameObject pref_Mummy;

    [SerializeField] float candyChance = 0.5f;

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

    public Vector3 GridToWorldPosition(Vector3 pos)
    {
        return new Vector3(pos.x * tileSize - (tileSize * cols / 2), pos.y * tileSize - (tileSize * rows / 2), 0);
    }

    public bool VerifyPosition(int x, int y)
    {
        return grid[x, y].GetComponent<GridObj>().GetState() != 1;
    }




    //level generation stuff oh boy here we go
    void GenerateLevel()
    {
        grid_modules = new int[4, 4];
        //set all modules to -1
        for (int i = 0; i < grid_modules.GetLength(0); i++)
            for (int j = 0; j < grid_modules.GetLength(1); j++)
                grid_modules[i, j] = -1;


        SelectModules();

        FillGrid();

        GenerateOutline();
    }

    //this has a lot of for loops because there's a specific order to how it generates, I hate it too
    void SelectModules()
    {
        //guarantee a downward module on top row
        grid_modules[Random.Range(0, grid_modules.GetLength(0)), grid_modules.GetLength(1) - 1] = 1;
        for (int c = 0; c < grid_modules.GetLength(0); c++)
            if (grid_modules[c, grid_modules.GetLength(1) - 1] == -1) grid_modules[c, grid_modules.GetLength(1) - 1] = 0;

        for (int r = grid_modules.GetLength(1) - 2; r >= 0; r--)
        {
            //link up previous row's downward modules to this row's upward modules
            for (int c = 0; c < grid_modules.GetLength(1); c++)
            {
                if (grid_modules[c, r + 1] == 1) grid_modules[c, r] = 2;
            }

            //guarantee a downward module on this row anywhere but the preivous row's link
            int mod_2_col = Random.Range(0, grid_modules.GetLength(0));
            while (grid_modules[mod_2_col, r] != -1) mod_2_col = Random.Range(0, grid_modules.GetLength(0));
            grid_modules[mod_2_col, r] = 1;

            //fill in the rest of the row
            for (int c = 0; c < grid_modules.GetLength(1); c++)
            {
                if (grid_modules[c, r] == -1) grid_modules[c, r] = Random.Range(0, 3);
            }
        }
    }



    //fills in each module with individual tiles, I can't think of a better name for it
    void FillGrid()
    {
        //TODO: change this to a 3D array for fancy code
        string[] module_file0 = File.ReadAllLines("Assets/Resources/modules_type0.txt");
        string[] module_file1 = File.ReadAllLines("Assets/Resources/modules_type1.txt");
        string[] module_file2 = File.ReadAllLines("Assets/Resources/modules_type2.txt");
        for (int r = 0; r < grid_modules.GetLength(0); r++)
        {
            for (int c = 0; c < grid_modules.GetLength(1); c++)
            {
                //TODO: make this support different numbers of modules for different module types
                int moduleIndex = Random.Range(0, 3) * 8;
                string[] module = new string[8];
                    switch (grid_modules[c, r])
                    {
                    case 0:
                        for (int i = 0; i < module.Length; i++)
                        {
                            module[i] = module_file0[moduleIndex + i];
                        }
                        FillModule(c * 10, r * 8, module);
                        break;
                    case 1:
                        for (int i = 0; i < module.Length; i++)
                        {
                            module[i] = module_file1[moduleIndex + i];
                        }
                        FillModule(c * 10, r * 8, module);
                        break;
                    case 2:
                        for (int i = 0; i < module.Length; i++)
                        {
                            module[i] = module_file2[moduleIndex + i];
                        }
                        FillModule(c * 10, r * 8, module);
                        break;
                    default:
                        break;
                }
            }
        }
    }



    void FillModule(int pX, int pY, string[] module)
    {
        for(int r = 0; r < module.Length; r++)
        {
            for(int c = 0; c < module[r].Length; c++)
            {
                char tileType = module[r][c];
                int tileInt;
                if (System.Int32.TryParse(tileType.ToString(), out tileInt))
                {
                    if (Random.Range(0, tileInt) == 0) grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
                    else grid[pX + c, pY + r].GetComponent<GridObj>().SetState(1);
                }
                else
                {
                    switch (tileType)
                    {
                        case '#':
                            grid[pX + c, pY + r].GetComponent<GridObj>().SetState(1);
                            break;
                        case '.':
                            grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
                            break;
                        case 'c':
                            grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
                            if(Random.Range(0.0f,1.0f) < candyChance) Instantiate(pref_Candy, grid[pX + c, pY + r].transform.position, Quaternion.identity);
                            break;
                        case 'm':
                            grid[pX + c, pY + r].GetComponent<GridObj>().SetState(0);
                            Instantiate(pref_Mummy, grid[pX + c, pY + r].transform.position, Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    //creates a wall round the level
    void GenerateOutline()
    {
        for(int c = 0; c < grid.GetLength(0); c++)
        {
            grid[c, 0].GetComponent<GridObj>().SetState(1);
            grid[c, grid.GetLength(1)-1].GetComponent<GridObj>().SetState(1);
        }
        for (int r = 1; r < grid.GetLength(1)-1; r++)
        {
            grid[0, r].GetComponent<GridObj>().SetState(1);
            grid[grid.GetLength(0)-1, r].GetComponent<GridObj>().SetState(1);
        }
    }
}
