using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attendee : MonoBehaviour {

    public Genre favoriteGenre;
    public int happiness;
    private Genre lovedGenre;
    private NavMeshAgent nav;
    public Attendee(Genre genreName) {
        lovedGenre = genreName;
        happiness = 150;
    }


    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ReceiveTargetPlace(Vector3 target)
    {
        nav.destination = target;
    }
}
