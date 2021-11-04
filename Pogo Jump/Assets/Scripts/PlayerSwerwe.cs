using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwerwe : MonoBehaviour
{
    private Rigidbody myRb;
    public Animator playerAnim;
    private bool tapToStart;
    public static bool endGame;

    public bool isMoving = true; //jump kısımlarında bunu kapatıp devam ettiricem

    public int EndJumpPlaceX; // Kaç x'e Zıplayacak

    [Header("Player Movement")]
    public float impulsForce = 8f;
    public float downforce = 10f;
    public float speed = 25f;

    [Header("Level Collectables")]
    public Text energyCountText;
    public Image energyBarImage;
    private int energyCount;

    [Header("Swerwe Manager")]
    [SerializeField] private float swerveSpeed = 4f;
    [SerializeField] private float maxSwerveAmount = 4f;
    private SwerveInputSystem _swerveInputSystem;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    void Start()
    {
        DOTween.Init();
        myRb = GetComponent<Rigidbody>();

        tapToStart = false;
        endGame = false;
        // energyBarImage = GameObject.Find("DolanEnerjiBarı").gameObject.GetComponent<Image>();
    }


    void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);

        if (tapToStart && !endGame && isMoving) //hareket
        {
            //icemanAnim.SetBool("Running", true); 
            transform.Translate(swerveAmount, 0, 0);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
        if (Input.GetMouseButtonDown(0) && !tapToStart) //taptostart
        {
            GameManager.instance.StartGame();
            UıManager.instance.TapToStartUI();
            tapToStart = true;
        }
        if (energyCount == 5)
        {
            Debug.Log("Bu kısma girdik ve enerji sayısı şu " + energyCount);
            EndJumpPlaceX = energyCount;  //10x kısmına zıplasın
            energyBarImage.DOFillAmount(0.240f, 0.3f).SetEase(Ease.Linear);
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && !endGame)
            StartCoroutine(ConstantJump(myRb, impulsForce)); // yere değdiği sürece zıplasın
        
        if (other.gameObject.tag == "FlipJump")
        {
            StartCoroutine(MiddleJump(myRb, 105f));
        }

        if (other.gameObject.tag == "Obstacle")
        {
            GameManager.instance.GameOver();
            endGame = true;
        }

        if (other.gameObject.tag == "EndTrigger")
        {
            GameManager.instance.Win();

            EndFlyJump10(transform);
            Debug.Log("hadi annecim zıpla");
            transform.DOMove(new Vector3(0.8f, 130, 1274), 3.5f);
            endGame = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Energy")
        {
            energyCount++;
            energyCountText.text = energyCount.ToString();
            Debug.Log(energyCount);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "JumpTrigger")
        {
            Debug.Log("trigger'a dokundu");
            playerAnim.SetBool("flipJump", true);
            StartCoroutine(MiddleJump(myRb, 20f));
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


    public IEnumerator EndFlyJump10(Transform transform) //ikinci paramereyi de ver nereye zıplayacağını bilsin
    {
        Debug.Log("e zıpla artık");
        isMoving = false;
        transform.DOMoveY(130, 2f); //dünya konumuna ayarla child konumda

        yield return new WaitForSeconds(0.1f);
    }
    /*
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
    */
}
