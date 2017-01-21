using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Toggle))]
public class TrackButton : MonoBehaviour {
    public MusicSample sample;

    private Toggle button;
    private PlayerController pController;
    // Use this for initialization
    void Start () {
        button = GetComponent<Toggle>();
        pController = FindObjectOfType<PlayerController>();
        //button.OnSelect.AddListener(ExecuteTask);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void ExecuteTask()
    {
        pController.PutTrack(sample,0);
    }
}
