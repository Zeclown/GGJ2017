using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("m"))
        {
            MusicSample newMusic = new MusicSample();
            newMusic.genre = Genre.Lizards;
            newMusic.soundEvent="ON_DownTempo_Bassline";
            newMusic.stopEvent = "OFF_DownTempo_Bassline";
            PutTrack(newMusic,0);
        }
        if (Input.GetKeyDown("n"))
        {
            MusicSample newMusic = new MusicSample();
            newMusic.genre = Genre.Lizards;
            newMusic.soundEvent = "ON_DownTempo_Snare";
            newMusic.stopEvent = "OFF_DownTempo_Snare";
            PutTrack(newMusic, 1);
        }
        if (Input.GetKeyDown("b"))
        {

            MusicSample newMusic = new MusicSample();
            newMusic.genre = Genre.Lizards;
            newMusic.soundEvent = "ON_DownTempo_Bassline_Pad";
            newMusic.stopEvent = "OFF_DownTempo_Bassline_Pad";
            PutTrack(newMusic, 2);
        }
       
    }
    public void PutTrack(MusicSample newSample, int position)
    {
        MusicPlayer.instance.PutTrack(newSample,position);

    }
    public void RemoveTrack(int position)
    {
        MusicPlayer.instance.RemoveTrack(position);
    }
}
