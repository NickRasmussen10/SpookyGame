using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObj : MonoBehaviour
{
    int state = 0;
    public int GetState() { return state; }
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(int pState)
    {
        state = pState;
        switch (state)
        {
            case 0:
                sprite.color = Color.white;
                break;
            case 1:
                sprite.color = Color.black;
                break;
            default:
                break;
        }
    }
}
