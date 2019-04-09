using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spawn : MonoBehaviour {

    
    public GameObject enemy1;
    public GameObject enemy2;
    [SerializeField]
    private GameObject[] enemyPrefabs1;
    [SerializeField]
    private GameObject[] enemyPrefabs2;
    


	// Use this for initialization
	void Start () {
        live.allowSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (live.allowSpawn)
        {
            musuhMuncul1();
            live.allowSpawn = false;
        }
        
        if (live.allowSpawn2)
        {
            musuhMuncul2();
            live.allowSpawn2 = false;
        }
		
	}

    void musuhMuncul1()
    {
        int r = Random.Range(0, 1);

        enemy1 = Instantiate(enemyPrefabs1[r], new Vector3(100, 297, 0), Quaternion.identity) as GameObject;
        enemy1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        
    }

    void musuhMuncul2()
    {
        int r = Random.Range(0, 1);

        enemy2 = Instantiate(enemyPrefabs2[r], new Vector3(-100, 297, 0), Quaternion.identity) as GameObject;
        enemy2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

}
