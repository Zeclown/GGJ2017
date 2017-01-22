using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour {
    public Text clockText;
    public GameObject arrow1;
    public GameObject arrow2;
    private bool tutorial1Completed = false;
    private bool tutorial2Completed = false;
    private bool tutorial3Completed = false;
    // Use this for initialization
    void Start () {
		if(GameManager.instance.infiniteMode)
        {
            tutorial1Completed = true;
            tutorial2Completed = true;
            tutorial3Completed = true;
            clockText.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(!tutorial1Completed && arrow1.activeSelf==false)
        {
            arrow1.SetActive(true);
            GetComponent<Animation>().Play("Tutorial1Arrow");
        }
       
        clockText.text = ((int)GameManager.instance.GameDuration-(int)GameManager.instance.timePlayed).ToString();
    }
    public void CompleteTutorial1()
    {
        if (!tutorial1Completed)
        {
            tutorial1Completed = true;
            arrow1.SetActive(false);
            StartCoroutine("StartTutorial2");
        }
        else if(!tutorial3Completed && tutorial2Completed)
        {
            tutorial3Completed = true;
            arrow1.SetActive(false);
        }
    }
    IEnumerator StartTutorial2()
    {
        yield return new WaitForSeconds(1.5f);
        arrow2.SetActive(true);
        GetComponent<Animation>().Play("TutorialArrow2");
        yield return new WaitForSeconds(4.5f);
        GetComponent<Animation>().Stop();
        arrow2.SetActive(false);
        tutorial2Completed = true;
        yield return new WaitForSeconds(6);
        if (FindObjectOfType<FolderButton>().opened)
        {
            arrow1.SetActive(true);
            GetComponent<Animation>().Play("Tutorial1Arrow");
        }
        else
            tutorial3Completed = true;
    }
}
