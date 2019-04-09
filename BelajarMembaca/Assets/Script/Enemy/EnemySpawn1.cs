using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class EnemySpawn1 : MonoBehaviour {

    public AudioClip eDestroySoundEfeck;
    //public AudioClip eGagalSoundEfeck;
    public GameObject audioPrefab;
    public GameObject panelStartGame;


    private GCSpeechRecognition _speechRecognition;

    public GameObject explotionEnemy;
    public GameObject EnemyObject1;
    public GameObject EnemyObject2;

    private JsonData kata1 = null;
    private JsonData kata2 = null;
    private int index_kata = -1;
    private int start = 0;

    //public int addScore;
    public string levelKata;
    //public int awalLive;
    public Text pesan;
    public Text pesan1;
    public Text kataMuncul1;
    public Text kataMuncul2;

    


    [SerializeField]
    private GameObject[] enemyPrefabs1;
    [SerializeField]
    private GameObject[] enemyPrefabs2;


    private float currentTime;


    List<float> remainingPositions = new List<float>();
    private int waveIndex;
    float xPos =0;
    int rand;

	// Use this for initialization
	void Start () {


        _speechRecognition = GCSpeechRecognition.Instance; // 1
        _speechRecognition.RecognitionSuccessEvent += RecognitionSuccessEventHandler;
        _speechRecognition.NetworkRequestFailedEvent += SpeechRecognizedFailedEventHandler;
        _speechRecognition.SetLanguage(Enumerators.LanguageCode.id_ID);
        _speechRecognition.StartRecord(true); // 3
        pesan.text = "Silahkan Ucapkan kata";
        panelStartGame.SetActive(true);

        //string url = "http://nanarmk23.000webhostapp.com/kata.php?level=1";

        string url1 = "http://nanarmk23.000webhostapp.com/kata.php?level=1";
        string url2 = "http://nanarmk23.000webhostapp.com/kata.php?level=2";
        string json_kata1 = GET(url1);
        string json_kata2 = GET(url2);

        JsonData kata_baru1 = JsonMapper.ToObject(json_kata1); // belum diacak
        JsonData kata_baru2 = JsonMapper.ToObject(json_kata2); // belum diacak
        kata1 = algorFisherYates(kata_baru1);
        kata2 = algorFisherYates(kata_baru2);

        score.ScoreAmount = 0;

        start = 0;
        live.allowSpawn = true;
        live.allowSpawn2 = true;

        Time.timeScale = 1;

        //currentTime = 0;
        
	}
	
	
	void Update () {
       
        if (start == 1 )
        {
            if (live.liveAmount > 0)
            {
                if (live.allowSpawn && index_kata < kata1.Count - 1)
                {
                    SpawnEnemy1();
                    live.allowSpawn = false;
                }

                if(live.allowSpawn2 && index_kata < kata2.Count - 1)
                {
                    SpawnEnemy2();
                    live.allowSpawn2 = false;
                }
            }
            else
            {
                _speechRecognition.StopRecord();// 4
            }
        }
	}

    void SpawnEnemy1()
    {
        kataMuncul1.text = kata1[++index_kata].ToString();

        print(live.liveAmount);
        int r = Random.Range(0, 1);

        EnemyObject1 = Instantiate(enemyPrefabs1[r], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

        EnemyObject1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    void SpawnEnemy2()
    {
        kataMuncul2.text = kata2[++index_kata].ToString();
        

        print(live.liveAmount);
        int r = Random.Range(0, 1);

        EnemyObject2 = Instantiate(enemyPrefabs2[r], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

        EnemyObject2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }



    string GET(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
                // log errorText
            }
            throw;
        }
    }


    JsonData algorFisherYates(JsonData kata) {
        for (int i = 0; i < kata.Count - 1; i++) {
            int s = Random.Range(0, kata.Count - 1);
            JsonData wadah = kata[i];
            kata[i] = kata[s];
            kata[s] = wadah;
        }
        return kata;
    }


    int RollingHash(int basis, string kata)
    {
        int total = 0;
        byte[] asciibytes = System.Text.Encoding.ASCII.GetBytes(kata);
        for (int i = 0; i < asciibytes.Length; i++)
        {
            int k = asciibytes.Length - (i + 1);
            int hasil_basis = System.Convert.ToInt32(System.Math.Pow(basis, k));
            total += ((int)asciibytes[i] * hasil_basis);
        }
        return total;
    }

    bool RabinKarp(string kataCari, string kataCocok)
    {
        int hsub = RollingHash(3, kataCocok);
        for (int i = 1; i <= (kataCari.Length - kataCocok.Length + 1); i++)
        {
            int hs = RollingHash(3, kataCari.Substring(i - 1, kataCocok.Length));
            if (hs == hsub)
            {
                return true;
            }
        }
        return false;
    }

    private void SpeechRecognizedFailedEventHandler(string obj, long requestIndex)
    {
        
        pesan.text = "Input suara Gagal";
        pesan1.text = "Suara Tidak Dikenal Silahkan Coba Lagi";
       // GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
       // instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eGagalSoundEfeck);
       // Destroy(instantiateAudioPrefab, 2);

    }

    // 5
    private void RecognitionSuccessEventHandler(RecognitionResponse obj, long requestIndex)
    {
        pesan.text = "event succeess";
        if (obj != null && obj.results.Length > 0)
        {
            pesan.text = "Kata yang anda baca: " + obj.results[0].alternatives[0].transcript;
            if (RabinKarp(kataMuncul1.text.ToLower(), obj.results[0].alternatives[0].transcript.ToLower()))
            {
                pesan1.text = "Kata yang Kamu Ucapkan Benar";
                Enemy enemy = GameObject.FindObjectOfType(typeof(Enemy)) as Enemy;
                //enemy.berhasilbaca();
                GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
                instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
                Destroy(instantiateAudioPrefab, 2);
                Destroy(EnemyObject1);
                live.allowSpawn = true;
                Mover s = GameObject.FindObjectOfType(typeof(Mover)) as Mover;
                score.ScoreAmount += s.addPoint;

            }
            else if (RabinKarp(kataMuncul2.text.ToLower(), obj.results[0].alternatives[0].transcript.ToLower()))
            {
                pesan1.text = "Kata yang Kamu Ucapkan Benar";
                Enemy1 enemy1 = GameObject.FindObjectOfType(typeof(Enemy1)) as Enemy1;
                //enemy1.berhasilbaca();
                GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
                instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
                Destroy(instantiateAudioPrefab, 2);
                Destroy(EnemyObject2);
                live.allowSpawn2 = true;
                Mover s = GameObject.FindObjectOfType(typeof(Mover)) as Mover;
                score.ScoreAmount += s.addPoint;
            }
            else
            {
                pesan1.text = "Kata yang Kamu Ucapkan salah" + System.Environment.NewLine +"Silahkan ulangi pengucapan kata";
               // GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
               // instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eGagalSoundEfeck);
               // Destroy(instantiateAudioPrefab, 2);
            }
        }
    }

    public void btnStart()
    {
        panelStartGame.SetActive(false);
        start = 1;
    }

}