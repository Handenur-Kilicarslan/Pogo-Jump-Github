
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwerwe : MonoBehaviour
{
    [Header("Animations")]
    public Animator playerAnim;

    [Header("Booleans")]
    public static bool endGame;
    private bool isMoving = true; //son jump kısımlarında bunu kapatıp devam ettiricem
    private bool tapToStart;
    public static bool animJumpBool = false;
    private bool isFlip = false;

    [Header("Locations")]
    public Transform endingStartPoint;
    public int EndJumpPlaceX; // Kaç x'e Zıplayacak

    [Header("Player Movement")]
    private Rigidbody myRb;
    public float impulsForce = 8f;
    public float downforce = 10f;
    public float speed = 25f;

    [Header("Level Objects Energy etc.")]
    public GameObject particleConfetti;
    public Image energyBarImage;
    private int energyCount;
    private float enBarFillFloat = 0; //energy bar filling float
    // public Text energyCountText;

    [Header("Tryings for Touch")]
    private Touch touch = new Touch();
    private float doubleTapTimer;
    private int tapCount;

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
        ResetPlayerPositionY(transform);
        tapToStart = false;
        endGame = false;
        particleConfetti.gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log("isFlip : " + isFlip);

        #region Swerwe Movement
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        #endregion
         
        #region Movement 
        
        if (tapToStart && !endGame && isMoving && Input.GetMouseButton(0)) //hareket
        {
            transform.Translate(swerveAmount, 0, 0);
            Movement();

            isFlip = true;
        }
        else if(tapToStart && !endGame && isMoving && isFlip && !Input.GetMouseButton(0))
        {
            Debug.Log("Zıplaması için gereken kısım");
            StartCoroutine(FlipJump(myRb, 35f));
            isFlip = false;
        }
        #endregion



        #region  Tap to Start
        if (Input.GetMouseButtonDown(0) && !tapToStart) //taptostart
        {
            GameManager.instance.StartGame();
            UıManager.instance.TapToStartUI();
            tapToStart = true;
        }
        #endregion

        #region Touch Try

        foreach (Touch touch in Input.touches)
        {
            if (touch.tapCount == 2)
            {
                Debug.Log("Touch sayısı : " +  touch.tapCount);
                StartCoroutine(FlipJump(myRb, 20f));
            }
        }
        #endregion

        if (energyCount == 5) //nereye zıplayacak
        {
            Debug.Log("Bu kısma girdik ve enerji sayısı şu " + energyCount);
            EndJumpPlaceX = energyCount;  //10x kısmına zıplasın
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Obstacle")
        {

            GameManager.instance.GameOver();
            ResetPlayerPositionY(transform);
            //playerAnim.SetBool("ölmeAnim", true);
            endGame = true;
        }

        if (other.gameObject.tag == "EndTrigger")
        {
            GameManager.instance.Win();
            particleConfetti.gameObject.SetActive(true);
            playerAnim.SetBool("flipJump", true);

            ResetPlayerPositionY(transform);

            Debug.Log("Energy count " + energyCount);
            energyCount -= 15;

            Debug.Log("Energy count " + energyCount);

            Transform endJumpTransform = GameManager.instance.EndJumpPlaceXPosition(energyCount);

            StartCoroutine(EndFlyJumpX(transform, endJumpTransform));
            endGame = true;
        }

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Energy")
        {
            Debug.Log("bu energy");
            energyCount++;
            Debug.Log(energyCount);

            enBarFillFloat += 0.04f;
            energyBarImage.DOFillAmount(enBarFillFloat, 0.3f).SetEase(Ease.Linear);

            other.transform.GetChild(0).gameObject.SetActive(true);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;

            //energyCountText.text = energyCount.ToString();
        }

        if (other.gameObject.tag == "JumpTrigger")
        {
            Debug.Log("Jump Trigger");
            ResetPlayerPositionY(transform);
            animJumpBool = true;
            Debug.Log("trigger'a dokundu");
            StartCoroutine(FlipJump(myRb, 20f));

            Handheld.Vibrate();
        }
    }


    void Movement()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public IEnumerator FlipJump(Rigidbody myRb, float upForce)
    {
        Debug.Log("Flip Jump Fonksiyonu");
        ResetPlayerPositionY2(transform, 44f);
        
        myRb.AddForce(Vector3.forward * 75f, ForceMode.Impulse);    
        playerAnim.SetBool("flipJump", true);

        yield return new WaitForSeconds(1f);

        playerAnim.SetBool("flipJump", false);

        yield return new WaitForSeconds(.4f);

        ResetPlayerPositionY2(transform, 44f); //havada uçma sorunu çözümü
        animJumpBool = false;
    }

    public IEnumerator EndFlyJumpX(Transform transform, Transform konum) //ikinci paramereyi de ver nereye zıplayacağını bilsin
    {
        Debug.Log("e zıpla artık");
        isMoving = false;

        yield return new WaitForSeconds(.3f);
        playerAnim.SetBool("flipJump", true);
        if (energyCount <= 4)
        {
            transform.DOMove(konum.position, 3.5f).SetEase(Ease.OutBack);
        }
        else if (energyCount > 4 && energyCount <= 7)
        {
            transform.DOMove(konum.position, 4.5f);
        }
        else if (energyCount > 7 && energyCount < 20)
        {
            transform.DOMove(konum.position, 5.5f);
        }

        playerAnim.SetBool("flipJump", false);

        //transform.DORotate(new Vector3(0, -120, 0), 2f);
    }

    public IEnumerator DelayThings(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        playerAnim.SetBool("flipJump", false);

        isFlip = false;

        animJumpBool = false;
    }

    void ResetPlayerPositionY(Transform transform)
    {
        transform.position = new Vector3(transform.position.x, 46f, transform.position.z);
    }

    void ResetPlayerPositionY2(Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}

/*
    if(other.gameObject.tag == "Ground" && !endGame)
        StartCoroutine(ConstantJump(myRb, impulsForce)); // yere değdiği sürece zıplasın
 
    public IEnumerator ConstantJump(Rigidbody rb, float impulsForce)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulsForce, ForceMode.Impulse);
        yield return new WaitForSeconds(.1f);
        rb.AddForce(Vector3.down * downforce, ForceMode.Impulse);
    }

     public IEnumerator AnotherJump(Rigidbody rb)
    {

        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(Vector3.up * 75f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        transform.DOLocalRotate(new Vector3(320, 0, 0), 1f, RotateMode.Fast);

        rb.constraints = RigidbodyConstraints.FreezeRotationZ;

    }

 */


 /* playerAnim.SetBool("flipJump", true);
    DelayThings(2f);
    playerAnim.SetBool("flipJump",false);
*/    

  
/*
if (tapToStart && !endGame && isMoving && !isFlip && !Input.GetMouseButton(0)) //  
{
    isFlip = true;
}
if (isFlip == true)
{   
    StartCoroutine(FlipJump(myRb, 35f));
    isFlip = false;
}
*/
