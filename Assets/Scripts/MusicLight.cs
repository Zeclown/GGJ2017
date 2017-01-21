using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Light))]
public class MusicLight : MonoBehaviour {
    float intensity;
    Light lightComp;
	// Use this for initialization
	void Start () {
        lightComp=GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

        lightComp.intensity = MusicPlayer.instance.GetBeat();
        
	}
}
