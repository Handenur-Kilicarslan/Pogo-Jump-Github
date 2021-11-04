using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UıManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject playPanel;
    public GameObject winPanel;
    public GameObject failPanel;


    void Start()
    {
        startPanel.SetActive(true);
        playPanel.SetActive(false);
        failPanel.SetActive(false);
        winPanel.SetActive(false);
    }


    public static UıManager instance;
    private void Awake()
    {
        instance = this;
    }
    
    void Update()
    {

    }

    public void TapToStartUI()
    {
        startPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void FaiUI()
    {
        playPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    public void WinUI()
    {
        playPanel.SetActive(false);
        winPanel.SetActive(true);
    }
}
