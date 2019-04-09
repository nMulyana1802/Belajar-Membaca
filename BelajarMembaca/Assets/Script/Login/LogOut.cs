using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void btnLogOut()
    {
        PlayerPrefs.DeleteKey("Username");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
