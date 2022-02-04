using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Right : MonoBehaviour
{
    private int movespeed = 5;
    private float xLim = 13.5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * movespeed);
        if (transform.position.x > xLim)
        {
            Destroy(gameObject);
        }
    }
}
