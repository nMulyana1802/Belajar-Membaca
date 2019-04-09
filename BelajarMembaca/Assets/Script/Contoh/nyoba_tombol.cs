using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class nyoba_tombol : MonoBehaviour {

    public InputField myid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseLevel()
    {
        userUpdate.getUser = myid.text;

        //Application.LoadLevelAsync ("Resources/Scenes/Load");
        //Application.LoadLevel(12);
        SceneManager.LoadScene(12);
    }


}

















