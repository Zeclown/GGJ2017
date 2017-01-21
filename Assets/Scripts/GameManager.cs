using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Starting,Playing,Paused,Ending}
public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public int Score { get { return (int)score; } }
    public int popularity;
    public GameState state;
    public float onFireTimeRequired=10;
    public int onFirePopularityRequired=90;
    public CrowdManager crowd;
    [HideInInspector]
    public bool onFire=false;
    private float onFireTimer=0;
    private float score;
    public float timePlayed=0;
    public int level;

    public float GameDuration = 180;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }
    private void Start()
    {
        state = GameState.Starting;
        StartGame();
    }
    private void Update()
    {
        
        if (state == GameState.Playing)
        {
            if(onFireTimer<=0)
            {
                onFire = true;
            } else
            {
                onFire = false;
            }

            if(popularity>=onFirePopularityRequired)
            {
                onFireTimer -= Time.deltaTime;
            } else {
                onFireTimer = onFireTimeRequired;
            }
            timePlayed += Time.deltaTime;
            score += Time.deltaTime * crowd.crowd.Count;
            ComputePopularity();

            if (score > 2000 && level == 1) {
                crowd.genreWaves[1].Activate(timePlayed);
                level = 2;
                Debug.Log("Unlock");
            } else if (score > 9000 && level == 2) {
                crowd.genreWaves[2].Activate(timePlayed);
                level = 3;
            }

            /*if (GameDuration<=timePlayed)
            {
                EndGame();
            }*/
        }
        
    }
    private void ComputePopularity()
    {
        int totalHappiness = 0;
        foreach (var item in crowd.crowd)
        {
            totalHappiness += (int)item.happiness;
        }
        if(crowd.crowd.Count!=0)
        popularity = totalHappiness / crowd.crowd.Count;
    }
    public void StartGame()
    {
        state = GameState.Playing;
        level = 1;
        crowd.genreWaves[0].Activate(timePlayed);
    }
    public void PauseGame()
    {
        state = GameState.Paused;
    }
    public void UnpauseGame()
    {
        state = GameState.Playing;
    }
    public void EndGame()
    {
        timePlayed = 0;

        state = GameState.Ending;
        GameInstance.instance.ToMainMenu();
    }
}
