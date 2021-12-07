using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTurner : MonoBehaviour
{
    public float speed;
    public bool direction; //default false
   

    void Update()
    {
        //yön değişkeni false ise döndür işte
        if (direction == false)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -1 * speed * Time.deltaTime);
        }

    }
}
