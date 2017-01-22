using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBeers : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Total Beers Earned: " + UnityEngine.Random.Range(50, 99);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
