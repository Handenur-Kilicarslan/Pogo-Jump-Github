using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTurner2 : MonoBehaviour
{
    public bool isHorizontal;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (isHorizontal == true)
        {
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(-1 * speed * Time.deltaTime, 0, 0);
        }
    }
}
