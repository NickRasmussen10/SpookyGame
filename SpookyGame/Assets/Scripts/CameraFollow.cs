using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform followObj;
    float zPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z;

        if (!followObj) followObj = FindObjectOfType<Player>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = followObj.position;
        newPos.z = zPos;
        transform.position = newPos;
    }
}
