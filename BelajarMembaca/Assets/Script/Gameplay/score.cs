using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class score : MonoBehaviour {

    private GCSpeechRecognition _speechRecognition;
    Text text;
    public static int ScoreAmount;
    public GameObject PanelWinner;
    public GameObject Winner;
    public GameObject AllertUlangi;
    public GameObject AllertMenu;
    public int targetScore;
    public bool conditionWin = false;

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    

    // Use this for initialization
    void Start () {
        _speechRecognition = GCSpeechRecognition.Instance;
        conditionWin = false;
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "Score : " +ScoreAmount.ToString();
        if (ScoreAmount >= targetScore)
        {
            _speechRecognition.StopRecord();
            Time.timeScale = 0;
            PanelWinner.SetActive(true);
            Winner.SetActive(true);
            conditionWin = true;
        }
    }

    public void btnLanjut(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void btnUlangi()
    {
        Winner.SetActive(false);
        AllertUlangi.SetActive(true);
    }

    public void btnIyaUlangi()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void btnTidakUlangi()
    {
        AllertUlangi.SetActive(false);
        Winner.SetActive(true);
    }

    public void btnMenu()
    {
        Winner.SetActive(false);
        AllertMenu.SetActive(true);
    }

    public void btnIyaMenu(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Time.timeScale = 1;
    }

    public void btnTidakMenu()
    {
        AllertMenu.SetActive(false);
        Winner.SetActive(true);
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
