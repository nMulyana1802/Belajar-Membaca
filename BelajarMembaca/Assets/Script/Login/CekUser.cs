using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

public class CekUser : MonoBehaviour {

    string Username, Pass;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    // Use this for initialization
    void Start () {
        cekingUser();
        login(Username, Pass);
        print("Username : " +Username);
        print("Password : " +Pass);
	}
	
    void cekingUser()
    {
        Username = PlayerPrefs.GetString("Username", "NoName");
        Pass = PlayerPrefs.GetString("Pass", "NoPass");
    }

    IEnumerator login(string _user, string _pass)
    {
        string url = "http://nanarmk23.000webhostapp.com/login.php";
        var form = new WWWForm();
        form.AddField("user", _user);
        form.AddField("password", _pass);
        //Loading.SetActive(true);

        var download = new WWW(url, form);

        yield return download;
        //Loading.SetActive(false);

        print(download.text);
        JsonData bacaData;
        bacaData = JsonMapper.ToObject(download.text);
        if ("" + bacaData["status"] == "berhasil")
        {
            PlayerPrefs.SetString("Username", _user);
            PlayerPrefs.SetString("Pass", _pass);
            StartCoroutine(LoadAsynchronously(3));
            //SceneManager.LoadScene("MainMenu");

        }
        else
        {
            SceneManager.LoadScene(1);
        }

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

    string GET(string CreateInsertURL)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CreateInsertURL);
        try
        {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            WebResponse errorResponse = ex.Response;

            using (Stream responseStream = errorResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                System.String errorText = reader.ReadToEnd();
                //log errorText
            }
            throw;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
