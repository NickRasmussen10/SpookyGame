using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    GameObject player;
    float magnetRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //proximity tracking should probably be done at player level instead of candy level but I'm a bad programmer
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Magnitude(player.transform.position - transform.position) < magnetRange)
        {
            StartCoroutine(MoveToPlayer());
        }
    }

    private IEnumerator MoveToPlayer()
    {
        float lerpVal = 0.0f;
        Vector3 start = transform.position;
        float x = 0;
        float y = 0;


        while(lerpVal < 1.0f)
        {
            x = Mathf.Lerp(start.x, player.transform.position.x, lerpVal);
            y = Mathf.Lerp(start.y, player.transform.position.y, lerpVal);

            transform.position = new Vector3(x, y);

            lerpVal += 0.05f;
            yield return null;
        }
        player.GetComponent<Player>().AddCandy(1);
        Destroy(gameObject);
    }
}
