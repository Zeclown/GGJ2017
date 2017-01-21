using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attendee : MonoBehaviour {

    public Genre favoriteGenre;
    public int happiness;
    private Genre lovedGenre;

    private NavMeshAgent nav;
    public Vector3 finalLocation;

    public bool leaving;

    public Attendee(Genre genreName) {
        lovedGenre = genreName;
        happiness = 150;
        leaving = false;
    }


    // Use this for initialization
    void Start () {
        if (!nav)
            nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if(finalLocation != -1 * Vector3.one) {
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
