using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;

public class LvUp : MonoBehaviour
{
    //string urlUpdate = "http://localhost/membaca/userUpdate2.php";
    string urlUpdate = "http://nanarmk23.000webhostapp.com/userUpdate.php";
    public string wF;
    public string wC;
    public int Level;
    public string cekLevel;
    public Text user;
    public Text userLevel;
    public static string getUser;
    private JsonData level = null;
    //private int index_alphabet = 0;



    // Use this for initialization
    void Start()
    {

        //string URL = "http://localhost/membaca/userSelect.php?" + "username=" + getUser;
        string URL = "http://nanarmk23.000webhostapp.com/userSelect.php?" + "username=" + getUser;
        string json_alphabet = GET(URL);
        level = JsonMapper.ToObject(json_alphabet);
        user.text = level[0]["username"].ToString();
        userLevel.text = level[0]["level"].ToString();
        wC = level[0]["username"].ToString();
        wF = "username";

        


    }

    // Update is called once per frame
    void Update()
    {


    }

    public void btnUpdate()
    {
        if (userLevel.text == cekLevel)
        {
            UpdateUser(Level, wF, wC);
        }
    }

    public void UpdateUser(int level, string wF, string wC)
    {
        WWWForm form = new WWWForm();

        form.AddField("editLevel", level);

        form.AddField("whereField", wF);
        form.AddField("whereCondition", wC);

        WWW www = new WWW(urlUpdate, form);
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
