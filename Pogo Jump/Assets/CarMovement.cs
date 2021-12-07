using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMovement : MonoBehaviour
{
    public float leftTime = 1;
    public float rightTime = 2;
    public int distanceX = 9;
    void Start()
    {
        DOTween.Init();
        GoThere();
    }


    void GoThere()
    {
        transform.DOMoveX(transform.position.x + distanceX, leftTime).OnComplete(() => GetBack());

    }

    void GetBack()
    {
        transform.DOMoveX(transform.position.x - distanceX, rightTime).OnComplete(() => GoThere());
    }
}
