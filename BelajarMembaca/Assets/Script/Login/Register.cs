using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class Register : MonoBehaviour {
    //public string url = "http://nanarmk23.000webhostapp.com/userInsert.php";
    //public InputField InputUsername, InputNama, InputPassword;
    public InputField InputUsername;
    public InputField InputNama;
    public InputField InputPassword;
    public int InputLevel = 1;
    public Text Warning;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AddUser(string _username, string _nama, string _password, int level)
    {
        string url = "http://nanarmk23.000webhostapp.com/userInsert.php";
        var form = new WWWForm();
        form.AddField("user", _username);
        form.AddField("nama", _nama);
        form.AddField("password", _password);

        var download = new WWW(url, form);

        yield return download;

        JsonData bacaData;

        bacaData = JsonMapper.ToObject(download.text);
        print(download.text);
        if ("" + bacaData["status"] == "berhasil")
        {
            //SceneManager.LoadScene("MainMenu");
            StartCoroutine(LoadAsynchronously(1));

        }
        else
        {
            Warning.text = "Coba Username yang lain";
        }
    }


    public void btnDaftar()
    {
        if (InputNama.text == "" || InputUsername.text == "" || InputPassword.text == "")
        {
            Warning.text = "Semua Field harus terisi";
        }
        else
        {
            StartCoroutine(AddUser(InputUsername.text, InputNama.text, InputPassword.text, InputLevel));
            
        }
    }// akhir btnDaftar

    public void btnKembali()
    {
        SceneManager.LoadScene("Login");
    }

    public void ScreenLoading(int sceneIndex)
    {
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
