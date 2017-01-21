using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour {

    private float minimumWaveDelay = 4;
    private float maximumWaveDelay = 5;

    //Seeded Creation Stuff
    private float MaxSeededCount;
    private List<float> totalSeedValue;
    //Seeded Creation Stuff

    public List<Attendee> crowd;
    public List<PopularityWave> genreWaves;
    public float nextArrivalWave;
    public float nextExitWave;

    [SerializeField]
    private GameObject attendeePrefab;

	// Use this for initialization
	void Start () {
        ScheduleInitialWaves(0/*10f*/);  //Adding a minimum time. 
        totalSeedValue = new List<float>();
        crowd = new List<Attendee>();
        //Debug.Log(genreWaves.Count);
    }

    private void ScheduleInitialWaves(float offset) {
        nextExitWave = Time.time + offset + UnityEngine.Random.Range(minimumWaveDelay, maximumWaveDelay);
        nextArrivalWave = nextExitWave + UnityEngine.Random.Range(minimumWaveDelay * 0.5f, maximumWaveDelay * 0.5f);
    }

    private void ScheduleNextArrivalWaves() {
        nextArrivalWave = Time.time + UnityEngine.Random.Range(minimumWaveDelay, maximumWaveDelay);
    }

    private void ScheduleNextExitWaves() {
        nextExitWave = Time.time + UnityEngine.Random.Range(minimumWaveDelay, maximumWaveDelay);
    }

    // Update is called once per frame
    void Update () {

        //TODO: Modulate speed of arrival on Popularity.

        if(Time.time >= nextExitWave) {
            MakeAttendeeLeave(3);
            Debug.Log(crowd.Count);
            ScheduleNextExitWaves();
        }
        if (Time.time >= nextArrivalWave) {
            MakeAttendeeArrive(3);
            Debug.Log(crowd.Count);
            ScheduleNextArrivalWaves();
        }

        //Debug.Log(genreWaves[0].frequencyWave.Evaluate(Time.time));
	}

    private void MakeAttendeeArrive(int countArriving) {
        UpdateCurrentPopularity();
        float randomValue;
        for(int i = 0; i < countArriving; i++) {
            randomValue = UnityEngine.Random.Range(0, MaxSeededCount);

            int ii = 0;
            while (randomValue > totalSeedValue[ii]) {
                //Debug.Log(randomValue + " " + totalSeedValue[ii]);
                ii++;
            }

            crowd.Add( Instantiate(attendeePrefab).GetComponent<Attendee>());
            crowd[crowd.Count - 1].favoriteGenre = genreWaves[ii].genreName;
        }
    }

    private void UpdateCurrentPopularity() {
        totalSeedValue.Clear();
        
        for(int i = 0; i < genreWaves.Count; i++) {
            if (i != 0)
                totalSeedValue.Add( totalSeedValue[i - 1] + genreWaves[i].frequencyWave.Evaluate(Time.time));
            else
                totalSeedValue.Add( genreWaves[i].frequencyWave.Evaluate(Time.time));
        }
        MaxSeededCount = totalSeedValue[totalSeedValue.Count-1];
    }

    private void MakeAttendeeLeave(int countLeaving) {
        if(crowd.Count <= countLeaving){
            crowd.Clear();
            Debug.Log("Crowd is now empty");
        } else {
            for(int i = 0; i < countLeaving; i++) {
                //TODO: Seed to pick more heavily in displeased persons.
                crowd.RemoveAt(UnityEngine.Random.Range(0, crowd.Count - 1));
            }
        }
    }
}
