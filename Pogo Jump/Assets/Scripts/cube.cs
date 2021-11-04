using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cube : MonoBehaviour
{

    Sequence seq;

    private Rigidbody myRb;
    // Start is called before the first frame update
    void Start()
    {

        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOMoveY(transform.position.y + 6f, 1f);
            transform.DOMoveZ(transform.position.z + 3f, 1f);

            transform.DOMoveY(transform.position.y + 6f, 1f);

        }
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            seq = DOTween.Sequence();
           // seq.Append(transform.DOMoveY(transform.position.y + 3f, 1f)).Append(TweenFunc());

            
        }
    }

    Tween TweenFunc()
    {
       return transform.DOMoveZ(transform.position.z + 3f, 1f).OnComplete(() => transform.DOMoveY(transform.position.y + 6f, 1f));

    }
    public IEnumerator FlipAndJump(Rigidbody rb)
    {

        yield return new WaitForSeconds(1f);
        rb.AddForce(Vector3.up * 35f, ForceMode.Impulse);
        // transform.Rotate(205f * Time.deltaTime, 0, 0);
        transform.DOLocalRotate(new Vector3(90, 0, 0), 2f);


    }
}
