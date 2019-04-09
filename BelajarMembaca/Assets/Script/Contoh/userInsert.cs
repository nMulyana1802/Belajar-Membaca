using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInsert : MonoBehaviour {
    string url = "http://localhost/membaca/userInsert.php";
    public string InputUsername, InputNama, InputPassword;
    public int InputLevel=1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddUser(InputUsername, InputNama, InputPassword, InputLevel);
        }
	}

    public void AddUser(string username, string nama, string password, int level)
    {
        WWWForm form = new WWWForm();
        form.AddField("addUsername", username);
        form.AddField("addNama", nama);
        form.AddField("addPassword", password);
        form.AddField("addLevel", level);


        WWW www = new WWW(url, form);
    }
}
