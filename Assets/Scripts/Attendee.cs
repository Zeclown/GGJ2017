using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attendee : MonoBehaviour {
    enum Mood { Happy,Neutral,Sad}
    private Mood mood;
    public Genre favoriteGenre;
    public GameObject heartSignal;
    public GameObject sadFaceSignal;
    public float happiness;
    //The rate at which the attendee lose happiness every tick;
    public float happinessFallOff=0.5f;
    //The rate at which the attendee lose happiness from other genres
    public float happinessLoseRate = 0.5f;
    //The rate at which the attendee gain happiness from prefered genres
    public float happinessGainRate=1;
    public float timeBetweenEmotes = 30;
    private NavMeshAgent nav;
    private float emoteCD;
    public Vector3 finalLocation;

    public bool leaving;
    public Attendee() {
        Setup();
    }
    public void Setup()
    {
        happiness = GameManager.instance?GameManager.instance.popularity:50;
        leaving = false;
        mood = GetMood();
        emoteCD = 0;
    }

    // Use this for initialization
    void Start () {
        Setup();
        if (!nav)
            nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateHappiness();
        if (finalLocation != -1 * Vector3.one) {
            if (Vector3.Distance(nav.destination, transform.position) < 3f) {
                nav.SetDestination(finalLocation);
                finalLocation = -1 * Vector3.one;
            }
        }	else if (leaving) {
            if (Vector3.Distance(nav.destination, transform.position) < 3f) {
                Destroy(gameObject, 0.3f);
            }
        }
        mood = GetMood();
        if(emoteCD<=0)
        {
            emoteCD = timeBetweenEmotes+Random.Range(0,3);
            if(mood==Mood.Happy)
            {
                GameObject newHeart = Instantiate(heartSignal);
                newHeart.transform.position = transform.position+Vector3.up;
                newHeart.GetComponent<Animation>().Play();
                Destroy(newHeart, 2);
            }
            else if(mood == Mood.Sad)
            {
                GameObject newHeart = Instantiate(sadFaceSignal);
                newHeart.transform.position = transform.position + Vector3.up;
                newHeart.GetComponent<Animation>().Play();
                Destroy(newHeart, 2);
            }
        }
        emoteCD -= Time.deltaTime;

	}
    Mood GetMood()
    {
        if(happiness>=90)
        {
            return Mood.Happy;
        }
        else if(happiness<=10)
        {
            return  Mood.Sad;
        }
        else
        {
            return  Mood.Neutral;
        }
    }
    public void CalculateHappiness()
    {
        happiness -= Time.deltaTime* happinessFallOff;
        for (int i = 0; i < 3; i++)
        {
            Genre currentGenre = (Genre)i;
            if (currentGenre == favoriteGenre)
            {
                happiness += (MusicPlayer.instance.GetGenreLevel(currentGenre) * Time.deltaTime*happinessGainRate);

            }
            else
            {
                happiness -= (MusicPlayer.instance.GetGenreLevel(currentGenre) * Time.deltaTime * happinessLoseRate);
            }
        }
        happiness=Mathf.Clamp(happiness, 0, 100);
      
    }
    public void Leave(Vector3 destination) {
        leaving = true;
        ReceiveTargetPlace(destination);
    }

    public void ReceiveTargetPlace(Vector3 target)
    {
        if(!nav)
            nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target);
    }
}
