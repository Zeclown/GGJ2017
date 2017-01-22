using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopularityBar : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       GetComponent<Slider>().value= GameManager.instance.popularity/100.0f;

    }
}
