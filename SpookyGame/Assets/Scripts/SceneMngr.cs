using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMngr : MonoBehaviour
{
    // Variables

    private Player playerRef;
    private List<Enemy> enemies;

    private int difficultyLevel = 1;

    [SerializeField] private float timeTilDifficultyInc;

    // Getters and Setters
    public int DifficultyLevel { get { return difficultyLevel; } }
    public float TimeTilDifficultyInc { get { return timeTilDifficultyInc; } }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
