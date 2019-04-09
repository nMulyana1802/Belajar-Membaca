using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Net;
using System.IO;

public class nyoba : MonoBehaviour {
    public Text mytext;
    private JsonData alphabets = null;
    public string nama;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator select(string username)
    {
        string url = "http://localhost/membaca/userSelect.php";
        var form = new WWWForm();
        form.AddField("username", username);
        var download = new WWW(url, form);

        yield return download;
        //string user = "a";
        StartCoroutine(select(nama));

        string json = GET(url);
        alphabets = JsonMapper.ToObject(json);

        mytext.text = json;


    }

    string GET(string url)
    {
        /*
        WebClient client = new WebClient();
        string reply = client.DownloadString(url);

        return reply;
        */
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
