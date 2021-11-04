using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PlayCam;

    public GameObject FailCam;

    public GameObject WinCam;

    [Header("Objecs")]
    public GameObject confetti;

    void Start()
    {

    }

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {

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
}
