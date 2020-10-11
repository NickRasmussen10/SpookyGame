using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform followObj;
    float zPos = 0;

    [SerializeField] Vector2 deadZone;

    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z;

        if (!followObj) followObj = FindObjectOfType<Player>().gameObject.transform;

        Vector3 startPos = followObj.position;
        startPos.z = zPos;
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        Vector3 camToFollow = followObj.position - transform.position;

        //deadzone stuff, not gonna lie I don't really know why it works I just messed with the numbers until it did
        if (camToFollow.x > deadZone.x) newPos.x += followObj.position.x - transform.position.x - deadZone.x;
        else if (camToFollow.x < -deadZone.x) newPos.x += followObj.position.x - transform.position.x + deadZone.x;

        if (camToFollow.y > deadZone.y) newPos.y += followObj.position.y - transform.position.y - deadZone.y;
        else if (camToFollow.y < -deadZone.y) newPos.y += followObj.position.y - transform.position.y + deadZone.y;

        transform.position = newPos;
        //Vector3 newPos = followObj.position;
        //newPos.z = zPos;
        //transform.position = newPos;


        //game close on esc (this should really be somewhere else but who's gonna stop me?
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
