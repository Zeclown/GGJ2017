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
    public bool infiniteMode = false;
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
        AkSoundEngine.PostEvent("Crowd_Boos",gameObject);
        AkSoundEngine.PostEvent("Crowd_Cheers", gameObject);
    }
    private void Update()
    {
        if (Input.GetButtonUp("Exit")) {
            Application.Quit();
        }
        if (crowd)
        {
            HandleCrowdSounds();
            if (state == GameState.Playing)
            {
                if (onFireTimer <= 0)
                {
                    onFire = true;

                }
                else
                {
                    onFire = false;
                }

                if (popularity >= onFirePopularityRequired)
                {
                    onFireTimer -= Time.deltaTime;
                }
                else
                {
                    onFireTimer = onFireTimeRequired;
                }
                timePlayed += Time.deltaTime;
                score += Time.deltaTime * crowd.crowd.Count;
                ComputePopularity();
                if (!infiniteMode)
                {
                    if (score > 200 && level == 1)
                    {
                        FindObjectOfType<SoundboardUI>().UnlockFolk();
                        level = 2;
                    }
                    if (score > 220 && level == 2)
                    {
                        level = 3;
                        crowd.genreWaves[1].Activate(timePlayed);
                        Debug.Log("Unlock");
                    }
                    else if (score > 1000 && level == 3)
                    {
                        FindObjectOfType<SoundboardUI>().UnlockMetal();
                        crowd.genreWaves[2].Activate(timePlayed);
                        level = 4;
                    }

                    if (GameDuration <= timePlayed)
                    {
                        EndGame();
                    }
                }
            }
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
    public void HandleCrowdSounds()
    {
        int happyCount=0;
        int madCount=0;
        if (crowd)
        {
            foreach (var guy in crowd.crowd)
            {
                if (guy)
                {
                    if (guy.GetComponent<Attendee>().happiness > 30)
                    {
                        happyCount++;
                    }
                    else
                    {
                        madCount++;
                    }
                }
            }
            if (happyCount >= 10)
            {
                AkSoundEngine.SetSwitch("Crowd_Cheers", "Big_Crowd", gameObject);
            }
            else if (happyCount >= 2)
            {
                AkSoundEngine.SetSwitch("Crowd_Cheers", "Small_Crowd", gameObject);
            }
            else
            {
                AkSoundEngine.SetSwitch("Crowd_Cheers", "Solo", gameObject);
            }

            if (madCount >= 10)
            {
                AkSoundEngine.SetSwitch("Crowd_Boos", "Big_Crowd", gameObject);
            }
            else if (happyCount >= 2)
            {
                AkSoundEngine.SetSwitch("Crowd_Boos", "Small_Crowd", gameObject);
            }
            else
            {
                AkSoundEngine.SetSwitch("Crowd_Boos", "Solo", gameObject);
            }
        }
    }
    public void StartGame()
    {
        state = GameState.Playing;
        crowd.genreWaves[0].Activate(timePlayed);
        if (infiniteMode)
        {
            level = 4;
            crowd.genreWaves[1].Activate(timePlayed);
            crowd.genreWaves[2].Activate(timePlayed);
            StartCoroutine("ToggleInfinite");
        }
        else
        {
            level = 1;
            crowd.genreWaves[1].Deactivate();
            crowd.genreWaves[2].Deactivate();
        }
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
        GameInstance.instance.ToEndGame();
    }
    IEnumerator ToggleInfinite()
    {
        if (FindObjectOfType<SoundboardUI>())
        {
            yield return new WaitForSeconds(2);
            FindObjectOfType<SoundboardUI>().UnlockMetal();
            yield return new WaitForSeconds(2);
            FindObjectOfType<SoundboardUI>().UnlockFolk();
        }
       
    }
}
