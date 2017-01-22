using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class SoundWaveParticle : MonoBehaviour {
    Renderer rendererComp;
	// Use this for initialization
	void Start () {
        rendererComp = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Color toTake = MusicPlayer.instance.GetCurrentColor();
        toTake.a = 0.3f;
        rendererComp.material.color = toTake;
        if (!MusicPlayer.instance.IsPlayingAnything() && GetComponent<ParticleSystem>().isPlaying)
            GetComponent<ParticleSystem>().Stop();
        else if(MusicPlayer.instance.IsPlayingAnything() && !GetComponent<ParticleSystem>().isPlaying)
            GetComponent<ParticleSystem>().Play();
    }
}
