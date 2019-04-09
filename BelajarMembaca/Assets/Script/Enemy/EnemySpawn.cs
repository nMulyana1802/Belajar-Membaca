using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class EnemySpawn : MonoBehaviour {

    public AudioClip eDestroySoundEfeck;
    //public AudioClip eGagalSoundEfeck;
    public GameObject audioPrefab;
    //public InputField tesText;
    public GameObject panelStartGame;
    //public GameObject GameO;
    //public GameObject PanelGameOver;
    //public GameObject AllertUlangi;
    //public GameObject AllertMenu;

    private GCSpeechRecognition _speechRecognition;
    //bool speechAktif = true;

    //public GameObject explotionEnemy;
    

    //public GameObject GameOver;
    public GameObject EnemyObject;
    private JsonData kata = null;
    private int index_kata = -1;
    private int start = 0;

    public int addScore;
    public string levelKata;
    //public int awalLive;
    public Text pesan;
    public Text pesan1;
    public Text kataMuncul;

    [SerializeField]
    private GameObject[] enemyPrefabs; // array enemy yang akan muncul
    

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
        _speechRecognition.StartRecord(true);
        pesan.text = "Silahkan Ucapkan kata";

        panelStartGame.SetActive(true);

        //string url = "http://nanarmk23.000webhostapp.com/kata.php?level=1";
        string url = levelKata;
        string json_kata = GET(url);

        JsonData kata_baru = JsonMapper.ToObject(json_kata); // belum diacak
        kata = algorFisherYates(kata_baru);

        
        //score Scr = GameObject.FindObjectOfType(typeof(score)) as score;
        
        start = 0;
        live.allowSpawn = true;
        //live.allowSpawn2 = true;
        //live.liveAmount = awalLive;
        Time.timeScale = 1;



        //      string word = "tar";
        //        int hash = RollingHash(101,word);
        //print(RabinKarp("basibisa","busa"));

        //print(kata[index_kata]);
        //audio = string.Join(" ", alphabets[index_alphabet]["audio_ind"] + "");

        //huruf.text = kata[index_kata]["huruf"] + ""; //bacaData["huruf"];
        
        currentTime = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        //PanelStart p = GameObject.FindObjectOfType(typeof(PanelStart)) as PanelStart;
        if (start == 1 )
        {
            
            if (live.liveAmount > 0)
            {
                // currentTime -= Time.deltaTime;
                //live m = GameObject.FindObjectOfType(typeof(live)) as live;
                if (live.allowSpawn && index_kata < kata.Count - 1)
                {
                    //int r = Random.Range(0, 1);
                    //GameObject gameObj = Instantiate(enemyPrefabs[r], new Vector3(xPos, transform.position.y, 0), Quaternion.identity);

                    SpawnEnemy();
                    live.allowSpawn = false;
                }
            }
            else
            {
                _speechRecognition.StopRecord();// 4

            }
                

            
        }
		
	}

    void SpawnEnemy()
    {
        //string kata_muncul = kata[++index_kata].ToString();
        //print(kata_muncul);

        kataMuncul.text = kata[++index_kata].ToString();

        //live m = GameObject.FindObjectOfType(typeof(live)) as live;
        print(live.liveAmount);
        int r = Random.Range(0, 1);

        EnemyObject = Instantiate(enemyPrefabs[r], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

        EnemyObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
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
            print("b: " + basis + "^ " + k);
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
        pesan1.text = "Suara Tidak Dikenal Silahkan Coba Lagi ";
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
            if (RabinKarp(kataMuncul.text.ToLower(), obj.results[0].alternatives[0].transcript.ToLower()))
            {
                pesan1.text = "Kata yang Kamu Ucapkan Benar";
                Enemy enemy = GameObject.FindObjectOfType(typeof(Enemy)) as Enemy;
                //enemy.berhasilbaca();
                GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
                instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
                Destroy(instantiateAudioPrefab, 2);

                //Instantiate(explotionEnemy, transform.position, transform.rotation);
                Destroy(EnemyObject);
                live.allowSpawn = true;
                Mover s = GameObject.FindObjectOfType(typeof(Mover)) as Mover;
                score.ScoreAmount += s.addPoint;
            } else
            {
                pesan1.text = "Kata yang Kamu Ucapkan salah" + System.Environment.NewLine + "Silahkan ulangi pengucapan kata";
               // GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
               // instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eGagalSoundEfeck);
               // Destroy(instantiateAudioPrefab, 2);
            }
        }  
    }


    /*
    public void btnDestroy()
    {
            if (RabinKarp(kataMuncul.text.ToLower(), ))
            {
                //pesan.text += " | ok ";
                Enemy enemy = GameObject.FindObjectOfType(typeof(Enemy)) as Enemy;
                enemy.berhasilbaca();
                GameObject instantiateAudioPrefab = Instantiate(audioPrefab, transform.position, transform.rotation);
                instantiateAudioPrefab.GetComponent<AudioSource>().PlayOneShot(eDestroySoundEfeck);
                Destroy(instantiateAudioPrefab, 2);

                //Instantiate(explotionEnemy, transform.position, transform.rotation);
                Destroy(EnemyObject);
                live.allowSpawn = true;
                Mover s = GameObject.FindObjectOfType(typeof(Mover)) as Mover;
                score.ScoreAmount += s.addPoint;
            }
        
    }
    */

        public void btnDes()
    {
        Destroy(EnemyObject);
        live.allowSpawn = true;
        Mover s = GameObject.FindObjectOfType(typeof(Mover)) as Mover;
        score.ScoreAmount += s.addPoint;
    }


    public void btnStart()
    {
        _speechRecognition.StartRecord(true);
        panelStartGame.SetActive(false);
        start = 1;
    }

}