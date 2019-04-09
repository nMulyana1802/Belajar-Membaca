using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStart : MonoBehaviour {
    public GameObject PanelStartGame;
    public int start = 0;

	public void btnStart()
    {
        PanelStartGame.SetActive(false);
        Time.timeScale = 1;
        start = 1;
    }
}
