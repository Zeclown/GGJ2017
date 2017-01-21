using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MusicSample
{
    
    public AkEvent soundEvent;
    public Genre genre;
};
public enum SoundSystemType { Recorder=0,Boombox=1,SmallAudioSystem=2}
public class MusicPlayer : MonoBehaviour {
    public static MusicPlayer instance;
    public SoundSystemType soundSystemType=0;
    const int MAX_TRACKS = 5;
    public int BPM = 40;
    [HideInInspector]
    public MusicSample?[] playing = new MusicSample?[MAX_TRACKS];
    private void Awake()
    {
        if(instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }
    private void Start()
    {

    }
    public float GetBeat()
    {
        return (Mathf.Cos(Time.time * (BPM / 60.0f) * 2 * Mathf.PI) + 1.0f)/ 2;
    }
    //Returns a percent based on the amount of track of a specific genre playing
    public float GetGenreLevel(Genre toGet)
    {
        int genreTracks =0;
        foreach (MusicSample? sample in playing)
        {
            if(sample!=null)
            {
                
                if(sample.Value.genre== toGet)
                {
                    genreTracks++;
                }
            }
        }
        return genreTracks / (float)MAX_TRACKS;
    }
    public void PutTrack(MusicSample newSample,int position)
    {
        playing[position] = newSample;
    }
    public void RemoveTrack(int position)
    {
        playing[position]=null;
    }



}
