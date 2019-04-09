/*Developed and provided by SpeedTutor - www.speed-tutor.com*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour
{
    public Image splashImage;
    public string loadLevel;

    string Username, Pass;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    IEnumerator Start()
    {
        cekingUser();
        splashImage.canvasRenderer.SetAlpha(0.0f);
        
        MenuLevel.getUser = Username;
        userUpdate.getUser = Pass;
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadScene(loadLevel);
        //login(Username, Pass);
        if(Username == "NoName")
        {
            
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(3);
        }

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

    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
