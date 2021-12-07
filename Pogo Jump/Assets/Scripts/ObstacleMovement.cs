using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMovement : MonoBehaviour
{
    public float upTime = 1;
    public float downTime = 2;
    public int distanceY = 9;
    void Start()
    {
        DOTween.Init();
        GoThere();
    }


    void GoThere()
    {
        transform.DOMoveY(transform.position.y + distanceY, upTime).OnComplete(() => GetBack());
        
    }
    
    void GetBack()
    {
        transform.DOMoveY(transform.position.y - distanceY, downTime).OnComplete(() => GoThere());
    }
    
}
