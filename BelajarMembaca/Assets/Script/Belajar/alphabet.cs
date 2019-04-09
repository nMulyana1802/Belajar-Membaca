using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;
using UnityEngine.Networking;

public class alphabet : MonoBehaviour {
    
    public GameObject gambar;
    //public AudioSource audio;
    public Sprite[] arrayGambar;
    public AudioClip[] arrayAudio;
    public Text huruf;
    int index;
    private JsonData alphabets = null;
    private int index_alphabet = 0;
    //private string[] audio;

    public GameObject loading;
    public Slider slider;
    public Text progressText;

    // Use this for initialization
    void Start() {
        string url = "http://nanarmk23.000webhostapp.com/alphabet.php";
        //StartCoroutine(GetText());
        string json_alphabet = GET(url);

        alphabets = JsonMapper.ToObject(json_alphabet);
        //print(json_alphabet);
        //print(alphabets[index_alphabet]);
        //audio = string.Join(" ", alphabets[index_alphabet]["audio_ind"] + "");

        huruf.text = alphabets[index_alphabet]["huruf"].ToString(); //bacaData["huruf"];
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];
        
    }


    // Update is called once per frame
    void Update() {

    }

    public void btnNext() {
        if (index_alphabet == 25)
        {

            index_alphabet = 0;
        }
        else
        {
            index_alphabet++;
        }



        huruf.text = alphabets[index_alphabet]["huruf"].ToString();
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];
    }

    public void btnback()
    {
        //if (index_alphabet > 0)
        //{
        //  index_alphabet--;
        //}

        if (index_alphabet == 0)
        {
            index_alphabet = 25;
        }
        else
        {
            index_alphabet--;
        }

        huruf.text = alphabets[index_alphabet]["huruf"].ToString();
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];
    }
    
    string GET(string url) {
        /*
        WebClient client = new WebClient();
        string reply = client.DownloadString(url);

        return reply;
        */
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        try {
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
    
    public void btnHome()
    {
        StartCoroutine(LoadAsynchronously(3));
    }
    //bool a = true;
    public void btnGambar()
    {
        /*
        string namaGambar = a ? "Background":"Sky";
        a = !a;
        int index = System.Array.FindIndex(tes, s => s.name == namaGambar);
        gambar.GetComponent<Image>().sprite = tes[index];
        */
    }


    public void btnAudio()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        
        int index = System.Array.FindIndex(arrayAudio, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        //print(index);
        audio.clip = arrayAudio[index];

        audio.Play();

    }

    public void btnBackMenu(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
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
}