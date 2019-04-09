using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void btnBelajar(int sceneIndex) {
        //SceneManager.LoadScene("kategori");
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    public void btnmulai(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void btnTentang(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void btnKeluar()
    {
        Application.Quit();
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
