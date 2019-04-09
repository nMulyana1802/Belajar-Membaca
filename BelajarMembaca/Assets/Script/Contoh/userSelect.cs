using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userSelect : MonoBehaviour {
    string url = "http://localhost/membaca/userSelect.php";
    public string[] userData;

    // Use this for initialization
    IEnumerator Start () {
        WWW user = new WWW(url);
        yield return user;
        string userDataString = user.text;
        System.Console.WriteLine(userDataString);
        userData = userDataString.Split(';');
        

        print (GetValueData (userData[0], "username:"));
	}

    string GetValueData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
