using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteObj
{
    public Sprite sprite;
    public float frequency;
}


public class GridObj : MonoBehaviour
{
    int state = 0;
    public int GetState() { return state; }
    SpriteRenderer sprite;
    [SerializeField] SpriteObj[] spr_Stone;
    [SerializeField] SpriteObj[] spr_Gates;

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
                foreach(SpriteObj spr in spr_Stone)
                {
                    if(Random.Range(0.0f,1.0f) <= spr.frequency)
                    {
                        sprite.sprite = spr.sprite;
                        break;
                    }
                }
                break;
            case 1:
                sprite.sprite = spr_Gates[0].sprite;
                break;
            //this is just setting up ground work for additional tile types
            case 2:
                sprite.color = Color.cyan;
                break;
            default:
                break;
        }
    }
}
