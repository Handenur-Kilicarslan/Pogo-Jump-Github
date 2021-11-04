using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Sequence seq;
    private Rigidbody myRb;
    private bool tapToStart = false;
    public static bool endGame = false;
    private float desiredPosition = 12;

    [Header("Player Movement")]
    public Swipe swipeControls;
    public float impulsForce = 8f;
    public float downforce = 10f;
    public float speed = 5f;

    void Start()
    {
        DOTween.Init();
        myRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (swipeControls.SwipeLeft)
        {
            transform.DOMoveX(-desiredPosition, 1); //.SetEase(Ease.Linear);
        }
        if (swipeControls.SwipeRight)
        {
            transform.DOMoveX(desiredPosition, 1);//.SetEase(Ease.Linear);
        }

        if (tapToStart && !endGame) //hareket
        {
            //icemanAnim.SetBool("Running", true);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
        }
        if (Input.GetMouseButtonDown(0) && !tapToStart)
        {
            GameManager.instance.StartGame();
            UıManager.instance.TapToStartUI();

            tapToStart = true;
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && !endGame)
            StartCoroutine(ConstantJump(myRb, impulsForce));

        if (other.gameObject.tag == "JumpTrigger")
        {
            StartCoroutine(MiddleJump(myRb, 85f));
            /*seq = DOTween.Sequence();
            myRb.velocity = Vector3.zero;
            seq.Append(transform.DOMoveY(transform.position.y + 6f, 1f)).Append(transform.DOMoveZ(transform.position.z + 5f, 2f));
            */
        }


        if (other.gameObject.tag == "FlipJump")
        {
            StartCoroutine(MiddleJump(myRb, 105f));
            Debug.Log("sen girmiyon mu buraya");
            //StartCoroutine(FlipAndJump(myRb));
            // myRb.constraints = RigidbodyConstraints.FreezeRotationX;
        }

        if (other.gameObject.tag == "Obstacle")
        {
            GameManager.instance.GameOver();
        }
    }



    void LeftSwp(Transform player)
    {
        player.transform.DOMoveX(0, 0.5f);
    }

    void RightSwp(Transform player)
    {
        player.transform.DOMoveX(1, 0.5f);
    }

    public IEnumerator FlipAndJump(Rigidbody rb)
    {

        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(Vector3.up * 75f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);

        transform.DOLocalRotate(new Vector3(320, 0, 0), 1f, RotateMode.Fast);
        //transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 5f).SetRelative(true).OnComplete(() => Destroy(transform.gameObject));
        //transform.Rotate(-5f * Time.deltaTime, 0, 0);

        rb.constraints = RigidbodyConstraints.FreezeRotationZ;

    }

    public IEnumerator MiddleJump(Rigidbody myRb, float upForce)
    {
        //transform.DOMoveY(transform.position.y + 6f, 1f).OnComplete(() => transform.DOMoveY(transform.position.y - 5f, 1f));

        myRb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        yield return new WaitForSeconds(.1f);
        myRb.AddForce(Vector3.down * 55f, ForceMode.Impulse);

    }

    public IEnumerator ConstantJump(Rigidbody rb, float impulsForce)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulsForce, ForceMode.Impulse);
        yield return new WaitForSeconds(.1f);
        rb.AddForce(Vector3.down * downforce, ForceMode.Impulse);
    }


}
