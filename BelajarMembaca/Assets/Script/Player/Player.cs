using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;

public class Player : MonoBehaviour {

    public static Player instance;

    [SerializeField]
    private float xlimit = 2.72f;

    [SerializeField]
    private float moveSpeed;

    //[SerializeField]
    //private Animator anim;

    private bool movingRight = true;
    private int direction = 1;
    private bool startMoving = false;

    public bool StartMoving { get { return startMoving; } }

    public void Awake()
    {
        if (instance == null)
            instance = this;

    }

    // Use this for initialization
    void Start () {
        //EnemySpawn es = GameObject.FindObjectOfType(typeof(EnemySpawn)) as EnemySpawn;
        //es.startRecognition();
        //EnemySpawn es = gameObject.transform.parent.gameObject ;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && startMoving == false)
        {
            startMoving = true;

        }

        if (startMoving == false) return;

        ChangeDirection();

        transform.position += Vector3.right * moveSpeed * Time.deltaTime * direction;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xlimit, xlimit), transform.position.y, transform.position.z);

       // anim.SetBool("Start", startMoving);
    }

    void ChangeDirection()
    {
        if(movingRight && transform.position.x >= xlimit)
        {
            movingRight = false;
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        if (!movingRight && transform.position.x <= -xlimit)
        {
            movingRight = true;
            direction = 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}
