using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 200));

        GUILayout.EndArea();
    }

}
