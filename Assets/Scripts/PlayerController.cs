using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
    public void PutTrack(MusicSample newMusic, int position)
    {
        MusicPlayer.instance.PutTrack(newMusic, position);
    }
    public void RemoveTrack(int position)
    {
        MusicPlayer.instance.RemoveTrack(position);
    }
}
