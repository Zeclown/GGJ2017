using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour {

    private const float minimumWaveDelay = 12;
    private const float maximumWaveDelay = 17;
    private const float minimumTrickleDelay = 3f;
    private const float maximumTrickleDelay = 7f;

    //Seeded Creation Stuff
    private float MaxSeededCount;
    private List<float> totalSeedValue;
    //Seeded Creation Stuff

    public List<Attendee> crowd;
    public List<PopularityWave> genreWaves;

    public float nextArrivalWave;
    public float nextExitWave;
    public float nextTrickleArrival;

    //Crowd position stuff
    public const float offsetCol = 1.5f;
    public const float offsetRow = 1.5f;
    public Vector3 offset;
    public const int firstRowSize = 5;
    List<int> occupied;
    [SerializeField]
    GameObject[] attendeePrefabs;
    [SerializeField]
    private GameObject attendeePrefab;
    [SerializeField]
    private Transform entryPoint;
    [SerializeField]
    private Transform exitPoint;

	// Use this for initialization
	void Start () {
        ScheduleInitialWaves(35f,20f);  //Adding a minimum time. 
        ScheduleTrickle(0);
        totalSeedValue = new List<float>();
        crowd = new List<Attendee>();
        occupied = new List<int>();
        //Debug.Log(genreWaves.Count);
    }

    private void ScheduleTrickle(float offset = 0) {
        nextTrickleArrival = Time.time + offset + UnityEngine.Random.Range(minimumTrickleDelay, maximumTrickleDelay);
    }

    internal Vector3 getExitToLeave(int ID) {
        for(int i = 0; i < occupied.Count; i++) {
            if(occupied[i] == ID) {
                occupied[i] = -1;
            }
        }
        return exitPoint.position;
    }

    private void ScheduleInitialWaves(float exitOffset,float entryOffset) {
        nextExitWave = Time.time + exitOffset + UnityEngine.Random.Range(minimumWaveDelay, maximumWaveDelay);
        nextArrivalWave = entryOffset + UnityEngine.Random.Range(minimumWaveDelay * 0.5f, maximumWaveDelay * 0.5f);
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
        if (GameManager.instance.state==GameState.Playing)
        {
            if (Time.time >= nextExitWave) {
                MakeAttendeeLeave((int)(UnityEngine.Random.Range(11, 13) - (GameManager.instance.popularity * 0.1f)));
                Debug.Log(crowd.Count);
                ScheduleNextExitWaves();
            }
            if (Time.time >= nextArrivalWave) {
                MakeAttendeeArrive((int)(GameManager.instance.popularity * 0.07f));
                Debug.Log(crowd.Count);
                ScheduleNextArrivalWaves();
            }

            if (Time.time >= nextTrickleArrival) {
                MakeAttendeeArrive(1);
                ScheduleTrickle();
            }
        }

        //Debug.Log(genreWaves[0].frequencyWave.Evaluate(Time.time));
	}

    private void MakeAttendeeArrive(int countArriving) {
        UpdateCurrentPopularity();
        float randomValue;

        randomValue = UnityEngine.Random.Range(0, MaxSeededCount);

        int ii = 0;
        while (randomValue > totalSeedValue[ii]) {
            //Debug.Log(randomValue + " " + totalSeedValue[ii]);
            ii++;
        }

        for (int i = 0; i < countArriving; i++) {
            if (crowd.Count == 30) {
                break;
            }

            crowd.Add( Instantiate(attendeePrefabs[ii]).GetComponent<Attendee>());
            //crowd[crowd.Count - 1].favoriteGenre = genreWaves[ii].genreName;
            int xpos, zpos;
            FindPosition(out xpos, out zpos, crowd[crowd.Count - 1].GetInstanceID());
            //Debug.Log(xpos + " " +zpos);
            crowd[crowd.Count - 1].gameObject.transform.position = entryPoint.position;
            crowd[crowd.Count - 1].ReceiveTargetPlace(new Vector3(entryPoint.position.x, 0, (zpos * offsetRow) - (xpos % 2 == 1 ? offsetRow * 0.5f : 0)) + offset);
            crowd[crowd.Count - 1].finalLocation = new Vector3((offsetCol * xpos), 0, (zpos * offsetRow) - (xpos % 2 == 1 ? offsetRow * 0.5f : 0)) + offset;
            //crowd[crowd.Count - 1].gameObject.transform.position.Set((offsetX * xpos), 0, /*(xpos * -offsetZ) + */((float)zpos * offsetZ)/* + (zpos % 2 == 0 ? offsetZ * 0.5f : 0)*/);
        }
    }

    private void FindPosition(out int xpos, out int zpos, int ID) {
        int rowSize = firstRowSize;
        int x = 0, z = 0;
        for(int i = 0;i < occupied.Count; i++) {
            if (occupied[i] == -1) {
                xpos = x;
                zpos = z;
                occupied[i] = ID;
                return;
            } else {
                z++;
                if(z >= rowSize) {
                    z = 0;
                    x++;
                    //rowSize++;
                }
            }
        }
        occupied.Add(ID);

        xpos = x;
        zpos = z;
    }

    private void UpdateCurrentPopularity() {
        totalSeedValue.Clear();
        
        for(int i = 0; i < genreWaves.Count; i++) {
            if (i != 0)
                totalSeedValue.Add( totalSeedValue[i - 1] + genreWaves[i].ReadFromCurve(GameManager.instance.timePlayed));
            else
                totalSeedValue.Add( genreWaves[i].ReadFromCurve(GameManager.instance.timePlayed));
        }
        MaxSeededCount = totalSeedValue[totalSeedValue.Count-1];
    }

    private void MakeAttendeeLeave(int countLeaving) {
        if(crowd.Count <= countLeaving){
            for (int i = 0; i < crowd.Count; i++)
                crowd[i].Leave(exitPoint.position);
            crowd.Clear();
            occupied.Clear();
            Debug.Log("Crowd is now empty");
        } else {
            int randomTemp;
            for(int i = 0; i < countLeaving; i++) {
                //TODO: Seed to pick more heavily in displeased persons.
                
                do { randomTemp = UnityEngine.Random.Range(0, crowd.Count - 1);} while (crowd[randomTemp].leaving);

                for (int ii = 0; ii< occupied.Count; ii++) {
                    if(occupied[ii] == crowd[randomTemp].GetInstanceID()) {
                        occupied[ii] = -1;
                        break;
                    }
                }
                crowd[randomTemp].Leave(exitPoint.position);
                crowd.RemoveAt(randomTemp);
            }
        }
    }
}
