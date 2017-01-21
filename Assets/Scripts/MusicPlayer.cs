using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MusicSample
{
    
    public AkEvent soundEvent;
    public Genre genre;
};
public class MusicPlayer : MonoBehaviour {
    public static MusicPlayer instance;
    const int MAX_TRACKS = 5;
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
    //Returns a percent based on the amount of track of a specific genre playing
    public float GetGenreLevel(Genre toGet)
    {
        int totalTracks = 0, genreTracks =0;
        foreach (MusicSample? sample in playing)
        {
            if(sample!=null)
            {
                totalTracks++;
                if(sample.Value.genre== toGet)
                {
                    genreTracks++;
                }
            }
        }
        return genreTracks / (float)totalTracks;
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
