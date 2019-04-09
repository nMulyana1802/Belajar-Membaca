using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject GameO;
    public GameObject PanelGameOver;
    public GameObject AllertUlangi;
    public GameObject AllertMenu;
    private GCSpeechRecognition _speechRecognition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (live.liveAmount < 0)
        {
            _speechRecognition.StopRecord();
            GameO.SetActive(true);
            PanelGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

  
    public void btnUlangi()
    {
        PanelGameOver.SetActive(false);
        AllertUlangi.SetActive(true);
    }

    public void btnTidakUlangi()
    {
        AllertUlangi.SetActive(false);
        PanelGameOver.SetActive(true);
    }

    public void btnIyaulangi()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void btnMenu()
    {
        PanelGameOver.SetActive(false);
        AllertMenu.SetActive(true);
    }

    public void btnTidakMenu()
    {
        AllertMenu.SetActive(false);
        PanelGameOver.SetActive(true);
    }

    public void btnIyaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
