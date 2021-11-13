using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Single Scene Data")]
    public Level[] levels;
    public GameStatus status = GameStatus.empty;
    public int whichLevel = 0;

    [Header("Player and Area in Levels")]
    public GameObject gameArea;
    public GameObject player;

    [Header("Cameras")]
    public GameObject PlayCam;
    public GameObject FailCam;
    public GameObject WinCam;

    [Header("Objecs")]
    public GameObject confetti;


    public Transform endingStartPoint;

    void Start()
    {
        DOTween.Init();
        Application.targetFrameRate = 60;
    }

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (status)
        {
            case GameStatus.empty:

                //bir prefabı var olan objeleri sahneye ekleyeceğim
                whichLevel = PlayerPrefs.GetInt("whichLevel");


                status = GameStatus.initalize;

                break;
            case GameStatus.initalize:
                break;
            case GameStatus.start:
                break;
            case GameStatus.stay:
                break;
            case GameStatus.restart:
                break;
            case GameStatus.next:
                break;
        }
    }

    public void StartGame()
    {
        // PlayCam.GetComponent<CinemachineVirtualCamera>().Priority = 12;
    }


    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        UıManager.instance.FaiUI();
        PlayerMovement.endGame = true;
        FailCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        //Fail Animasyonu
        Debug.Log("fail");
    }

    public void Win()
    {
        WinCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        confetti.SetActive(true);
        UıManager.instance.WinUI();
        Debug.Log("You Win");
    }


    //enerji sayısına göre konum dönecek
    public Transform EndJumpPlaceXPosition(int energyCount)
    {
        int n = energyCount;

        endingStartPoint.position = new Vector3(0.8f, 44, 1034.24f); //1x konumu

        //i yi 2 2 arttırabilirim

        for (int i = 0; i < n; i++)
        {
            endingStartPoint.position += new Vector3(0, 7.5f, 44.6f); //2şerliçıksın

            //endingStartPoint.position += new Vector3(0, 7.6f, 22.3f); //iki yer arası mesafe
        }

        return endingStartPoint;
    }

    /*
    public void Next()
    {

        whichLevel++;
        PlayerPrefs.SetInt("whichLevel", whichLevel);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //buildIndex'i tutturuyotuz.
         //Olası farklı senaryolarda -next level olmayışı gibi- sıkıntı çıkarmasın diye 

    //iceCubeCount = 0;// next level için küp sayısını 0'ladım.
    status = GameStatus.empty; // tekrar empty'e çekiyoruz.

        if (whichLevel >= levels.Length)
        {
            whichLevel--;
            PlayerPrefs.SetInt("randomLevel", 1);
        }
    }
     */




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

        //transform.DOMoveY(transform.position.y + 6f, 1f).OnComplete(() => transform.DOMoveY(transform.position.y - 5f, 1f));
    }
    */
}
