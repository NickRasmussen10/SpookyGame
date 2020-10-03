using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int rows;
    public int cols;
    public float tileSize = 1;

    [SerializeField] GameObject pref_GridTile;

    public GameObject[,] grid;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
