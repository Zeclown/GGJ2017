using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speakers : MonoBehaviour {
    Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        MusicPlayer.OnBeat += PlayBoom;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlayBoom()
    {
        if(MusicPlayer.instance.IsPlayingAnything())
            anim.Play("SpeakerBoom");
    }
}
