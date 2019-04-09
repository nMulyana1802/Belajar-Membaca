using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static int addScore;
    //public AudioClip eDestroySound;
    public AudioClip eDestroySoundEfeck;
    public GameObject audioPrefab;
    public GameObject explotionEnemy;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            //EnemySpawn m = GameObject.FindObjectOfType(typeof(EnemySpawn)) as EnemySpawn;
            //m.tes();
            //live s = GameObject.FindObjectOfType(typeof(live)) as live;

            // AudioSource audio = gameObject.AddComponent<AudioSource>();
            GameObject instantiateAudioPrefab =  Instantiate(audioPrefab, transform.position, transform.rotation);
            instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
            Destroy(instantiateAudioPrefab, 2);
            live.liveAmount -= 1;

            Instantiate(explotionEnemy, transform.position, transform.rotation);
            
            Destroy(gameObject);
            live.allowSpawn = true;
        }
    }

    public void berhasilbaca()
    {

        Instantiate(explotionEnemy, transform.position, transform.rotation);

    }

}
