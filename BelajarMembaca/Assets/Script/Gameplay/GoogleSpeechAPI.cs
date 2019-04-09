using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleSpeechAPI : MonoBehaviour {

    public Text pesan;

    private GCSpeechRecognition _speechRecognition;

    // Use this for initialization
    void Start () {
        _speechRecognition = GCSpeechRecognition.Instance; // 1
        _speechRecognition.RecognitionSuccessEvent += RecognitionSuccessEventHandler;
        _speechRecognition.NetworkRequestFailedEvent += SpeechRecognizedFailedEventHandler;
        _speechRecognition.SetLanguage(Enumerators.LanguageCode.id_ID);
        _speechRecognition.StartRecord(true);
        
        
    }
	
	// Update is called once per frame
	void Update () {
        EnemySpawn m = GameObject.FindObjectOfType(typeof(EnemySpawn)) as EnemySpawn;
        print(m.kataMuncul);
    }

    private void SpeechRecognizedFailedEventHandler(string obj, long requestIndex)
    {

        pesan.text = "Kata yang anda baca gagal : " + obj;

    }

    // 5
    private void RecognitionSuccessEventHandler(RecognitionResponse obj, long requestIndex)
    {
        pesan.text = "event succeess";
        if (obj != null && obj.results.Length > 0)
        {
            
            pesan.text = "Kata yang anda baca: " + obj.results[0].alternatives[0].transcript;
            /*
            if (RabinKarp(kataMuncul.text.ToLower(), obj.results[0].alternatives[0].transcript.ToLower()))
            {
                Destroy(EnemyObject);
                live.allowSpawn = true;
            }
            */
        }
    }
}
