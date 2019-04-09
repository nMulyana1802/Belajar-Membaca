using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;

public class MenuLevel : MonoBehaviour {

    //public GameObject loading;
    //public Slider slider;
    //public Text progressText;

    public GameObject lockLevel2;
    public GameObject lockLevel3;
    public GameObject lockLevel4;

    public Text user;
    public Text userLevel;
    public static string getUser;
    private JsonData level = null;
    private int index_alphabet = 0;

    // Use this for initialization
    void Start () {
        //string URL = "http://localhost/membaca/userSelect.php?" + "username=" + getUser;
        string URL = "http://nanarmk23.000webhostapp.com/userSelect.php?" + "username=" + getUser;
        string json_alphabet = GET(URL);
        level = JsonMapper.ToObject(json_alphabet);
        user.text = level[index_alphabet]["username"].ToString();
        userLevel.text = level[index_alphabet]["level"].ToString();
        print(getUser);


        if (userLevel.text == "2")
        {
            lockLevel2.SetActive(false);
        }
        else if (userLevel.text == "3")
        {
            lockLevel2.SetActive(false);
            lockLevel3.SetActive(false);
        }
        else if (userLevel.text == "4")
        {
            lockLevel2.SetActive(false);
            lockLevel3.SetActive(false);
            lockLevel4.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
       
	}

    /*
    IEnumerator LoadAsynchronously(int sceneIndex){

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
    */

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
