using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attendee : MonoBehaviour {

    public Genre favoriteGenre;
    public int happiness;
    private Genre lovedGenre;

    public Attendee(Genre genreName) {
        lovedGenre = genreName;
        happiness = 150;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
