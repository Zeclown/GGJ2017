﻿using System.Collections;
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
        rendererComp.material.color = MusicPlayer.instance.GetCurrentColor();

    }
}
