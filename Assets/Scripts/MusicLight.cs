using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Light))]
public class MusicLight : MonoBehaviour {
    public float intensity;
    Light lightComp;
    public bool genreUnified = true;
    public float defaultIntensity;
	// Use this for initialization
	void Start () {
        lightComp=GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

        if (MusicPlayer.instance.IsPlayingAnything()) {
            if(genreUnified)
                lightComp.color = MusicPlayer.instance.GetCurrentColor();
            lightComp.intensity = MusicPlayer.instance.GetBeat() * intensity;
        }else {
            lightComp.intensity = defaultIntensity;
        }
        
	}
}
