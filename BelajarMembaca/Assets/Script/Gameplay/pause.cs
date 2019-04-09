using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pause : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public GameObject panelMenuUI;
    public GameObject pauseMenuUI;
    public GameObject AllertUlangi;
    public GameObject AllertMainMenu;


    public bool paused;
    public void PauseGame()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
            panelMenuUI.SetActive(true);

        }
        else if (!paused)
        {
            pauseMenuUI.SetActive(false);
            panelMenuUI.SetActive(false);
            Time.timeScale = 1;
            
        }
    }

    public void btnMulai()
    {

        panelMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    

    public void btnUlangi()
    {
        pauseMenuUI.SetActive(false);
        AllertUlangi.SetActive(true);
    }

    public void btnTidakUlangi()
    {
        AllertUlangi.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ulangi()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void btnMainMenu()
    {
        pauseMenuUI.SetActive(false);
        AllertMainMenu.SetActive(true);
    }

    public void btnTidakMainMenu()
    {
        AllertMainMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void mainMenu(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Time.timeScale = 1;
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
