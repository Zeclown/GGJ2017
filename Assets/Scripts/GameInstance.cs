using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {
    public static GameInstance instance;
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
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }
    void ToMainMenu()
    {

    } 
}
