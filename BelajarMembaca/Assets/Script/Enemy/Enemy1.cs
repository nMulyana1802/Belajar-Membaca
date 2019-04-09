using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

    public static int addScore = 15;
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
            GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
            instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
            Destroy(instantiateAudioPrefab, 2);
            live.liveAmount -= 1;
            //score.ScoreAmount += 10;
            //Debug.Log("Collided with Ground");
            Destroy(gameObject);
            live.allowSpawn2 = true;
            //gameObject.SetActive(false);
        }
    }

    public void berhasilbaca()
    {

        Instantiate(explotionEnemy, transform.position, transform.rotation);

    }

}
