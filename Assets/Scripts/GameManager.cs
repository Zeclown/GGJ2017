using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Starting,Playing,Paused,Ending}
public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public int Score { get { return (int)score; } }
    public int popularity;
    public GameState state;
    public CrowdManager crowd;
    private float score;
    public float timePlayed=0;
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
            timePlayed += Time.deltaTime;
            score += Time.deltaTime * crowd.crowd.Count;
            ComputePopularity();
        }
    }
    private void ComputePopularity()
    {
        int totalHappiness = 0;
        foreach (var item in crowd.crowd)
        {
            totalHappiness += item.happiness;
        }
        if(crowd.crowd.Count!=0)
        popularity = totalHappiness / crowd.crowd.Count;
    }
    public void StartGame()
    {
        state = GameState.Playing;
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
        state = GameState.Ending;
    }
}
