using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kategori : MonoBehaviour {

    public GameObject loading;
    public Slider slider;
    public Text progressText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loading.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void btnPindah(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
}
