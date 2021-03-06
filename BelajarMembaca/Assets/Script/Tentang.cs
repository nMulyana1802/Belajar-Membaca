﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tentang : MonoBehaviour {

    public GameObject loading;
    public Slider slider;
    public Text progressText;

    public GameObject tentang;
    public GameObject credit;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void Pindah(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void btnTentang()
    {
        tentang.SetActive(true);
        credit.SetActive(false);
    }

    public void btnCredit()
    {
        tentang.SetActive(false);
        credit.SetActive(true);
    }
}
