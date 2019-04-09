using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;
public class kata : MonoBehaviour {

    public GameObject gambar;
    public Sprite[] arrayGambar;
    public AudioClip[] arrayAudio;
    //public GameObject audioPrefab;
    public Text huruf;
    private JsonData alphabets = null;
    private int index_alphabet=26;
    //private int audioPlay = 0;

    public GameObject loading;
    public Slider slider;
    public Text progressText;


    // Use this for initialization
    void Start () {
        string url = "http://nanarmk23.000webhostapp.com/alphabet.php";
        string json_alphabet = GET(url);
        
        alphabets = JsonMapper.ToObject(json_alphabet);
        print(alphabets[index_alphabet]);

        huruf.text = alphabets[ index_alphabet ]["huruf"] + ""; //bacaData["huruf"];
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];

    }

	
	// Update is called once per frame
	void Update () {
        
		
	}

    public void btnNext() {
        if (index_alphabet == alphabets.Count - 1)
        {

            index_alphabet = 26;
        }
        else
        {
            index_alphabet++;
        }

        
        
        huruf.text = alphabets[index_alphabet]["huruf"] + "";
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];
    }

    public void btnback()
    {
        //if (index_alphabet > 0)
        //{
        //  index_alphabet--;
        //}

        if (index_alphabet == 26)
        {
            index_alphabet = alphabets.Count - 1;
        }
        else
        {
            index_alphabet--;
        }
        
        huruf.text = alphabets[index_alphabet]["huruf"] + "";
        int index = System.Array.FindIndex(arrayGambar, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
        gambar.GetComponent<Image>().sprite = arrayGambar[index];
    }

    string GET(string url) {
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
                // log errorText
            }
            throw;
        }
    }

    public void btnHome()
    {
        StartCoroutine(LoadAsynchronously(3));
    }

    public void btnAudio()
    {
       
         AudioSource audio = gameObject.AddComponent<AudioSource>();
         int index = System.Array.FindIndex(arrayAudio, s => s.name == alphabets[index_alphabet]["huruf"].ToString());
         //print(index);
         audio.clip = arrayAudio[index];

         //GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
         //instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(arrayAudio[index]);
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
