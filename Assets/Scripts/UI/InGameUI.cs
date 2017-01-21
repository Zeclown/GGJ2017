using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour {
    public Text scoreText;
    public Text clockText;
    public Text popularityText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = GameManager.instance.Score.ToString();
        popularityText.text = GameManager.instance.popularity.ToString();
        clockText.text = ((int)GameManager.instance.GameDuration-(int)GameManager.instance.timePlayed).ToString();
    }
}
