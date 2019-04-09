using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
    private JsonData cekUser = null;
    public InputField UserNameLogin;
    public InputField PassLogin;
    public InputField myid;
    //public InputField UserRegis;
    //public InputField PassRegis;
    //public InputField NamaRegis;
    //public Text nama;
    //public Text level;
    //public GameObject MainMenu;
    // public GameObject LoginForm;
    //public GameObject RegisForm;
    //public GameObject Loading;
    public Text Warning;
    string Username, Pass;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator login(string _user, string _pass)
    {
        string url = "http://nanarmk23.000webhostapp.com/login.php";
        var form = new WWWForm();
        form.AddField("user",_user);
        form.AddField("password", _pass);
        //Loading.SetActive(true);
        
        var download = new WWW(url, form);

        yield return download;
       //Loading.SetActive(false);

        print(download.text);
        JsonData bacaData;
        bacaData = JsonMapper.ToObject(download.text);
        if(""+bacaData["status"]== "berhasil")
        {
            PlayerPrefs.SetString("Username", Username);
            PlayerPrefs.SetString("Pass", Pass);
            StartCoroutine(LoadAsynchronously(3));
            //SceneManager.LoadScene("MainMenu");

        } else
        {
            Warning.text = "Username/Password Salah";
        }

    }

    


    public void btnLogin()
    {
        if (UserNameLogin.text == "" || PassLogin.text == "")
        {
            Warning.text = "Semua Field harus terisi";
            
        } else
        {
            Username = UserNameLogin.text;
            Pass = PassLogin.text;
            //PlayerPrefs.SetString("Username", Username);
            //PlayerPrefs.SetString("Pass", Pass);
            MenuLevel.getUser = myid.text;
            userUpdate.getUser = myid.text;
            StartCoroutine(login(UserNameLogin.text, PassLogin.text));
            
        }
    }

    public void btnDaftar(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
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
}
