using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MusicSample
{

    public string soundEvent;
    public string stopEvent;
    public Genre genre;
};
public enum SoundSystemType { Recorder = 0, Boombox = 1, SmallAudioSystem = 2 }
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public SoundSystemType soundSystemType = 0;
    const int MAX_TRACKS = 5;
    public int BPM = 40;
    [HideInInspector]
    public MusicSample?[] playing = new MusicSample?[MAX_TRACKS];
    public delegate void FirstBeat();
    public static event FirstBeat OnFirstBeat;
    public delegate void Beat();
    public static event Beat OnBeat;
    private bool waitingOnBeat = true;
    private int beatCount;
    private void Awake()
    {
        if (instance)
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
    private void Update()
    {
        if (waitingOnBeat && GetBeat() > 0.94f)
        {
            waitingOnBeat = false;
            if (OnBeat!=null)
                OnBeat();
            if (beatCount % 4 == 0)
            {
                if (OnFirstBeat != null)
                    OnFirstBeat();
            }
            beatCount++;
        }
        waitingOnBeat = true;

    }
 
    public float GetBeat()
    {
        return (Mathf.Cos(GameManager.instance.timePlayed * (BPM / 60.0f) * 2 * Mathf.PI) + 1.0f) / 2;
    }
    public Color GetCurrentColor()
    {
        return Color.red;
    }
    public Color GetGenreColor(Genre genre)
    {
        switch (genre)
        {
            case Genre.Folk:
                return Color.red;
            case Genre.DownBeat:
                return Color.blue;
            case Genre.Metal:
                return Color.cyan;
            default:
                return Color.red;
        }
    }
    //Returns a percent based on the amount of track of a specific genre playing
    public float GetGenreLevel(Genre toGet)
    {
        int genreTracks = 0;
        foreach (MusicSample? sample in playing)
        {
            if (sample != null)
            {

                if (sample.Value.genre == toGet)
                {
                    genreTracks++;
                }
            }
        }
        return genreTracks / (float)MAX_TRACKS;
    }
    public void PutTrack(MusicSample newSample, int position)
    {
        if(playing[position].HasValue)
        {
            AkSoundEngine.PostEvent(playing[position].Value.stopEvent,gameObject);
        }
        playing[position] = newSample;
        AkSoundEngine.PostEvent(playing[position].Value.soundEvent, gameObject);
    }
    public void RemoveTrack(int position)
    {
        if (playing[position].HasValue)
        {
            AkSoundEngine.PostEvent(playing[position].Value.stopEvent, gameObject);
        }
        playing[position] = null;
    }



}
