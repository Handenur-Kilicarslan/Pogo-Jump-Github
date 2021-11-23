using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bengu : MonoBehaviour
{
    public GameObject[] diziKup = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartCoroutine(Hareket());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Hareket()
    {
        for (int i = 0; i < 3; i++)
        {
            diziKup[i].gameObject.transform.DOMoveX(2, 1f);
            yield return new WaitForSeconds(2f);

        }
    }
}
