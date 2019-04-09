using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class live : MonoBehaviour {

    Text text;
    public static int liveAmount;
    public static bool allowSpawn= true;
    public static bool allowSpawn2= true;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public GameObject GameOver;
    public GameObject GameO;
    public GameObject AllertUlangi;
    public GameObject AllertMenu;

    public int awalLive;
  
    // Use this for initialization
    void Start () {
        liveAmount = awalLive;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = liveAmount.ToString();

        if(liveAmount <= 0)
        {
            //Time.timeScale = 0;
            GameO.SetActive(true);
            GameOver.SetActive(true);
        }
        
	}

    public void btnUlangi()
    {
        GameOver.SetActive(false);
        AllertUlangi.SetActive(true);
    }

    public void btnTidakUlangi()
    {
        AllertUlangi.SetActive(false);
        GameOver.SetActive(true);
    }

    public void btnIyaulangi()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void btnMenu()
    {
        GameOver.SetActive(false);
        AllertMenu.SetActive(true);
    }

    public void btnTidakMenu()
    {
        AllertMenu.SetActive(false);
        GameOver.SetActive(true);
    }

    public void btnIyaMainMenu(int sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }


}
