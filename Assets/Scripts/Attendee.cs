using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attendee : MonoBehaviour {

    public Genre favoriteGenre;
    public float happiness;
    //The rate at which the attendee lose happiness every tick;
    public float happinessFallOff=0.5f;
    //The rate at which the attendee lose happiness from other genres
    public float happinessLoseRate = 0.5f;
    //The rate at which the attendee gain happiness from prefered genres
    public float happinessGainRate=1;

    private NavMeshAgent nav;
    public Vector3 finalLocation;

    public bool leaving;

    public Attendee() {
        Setup();
    }
    public void Setup()
    {
        happiness = GameManager.instance?GameManager.instance.popularity:50;
        leaving = false;
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
