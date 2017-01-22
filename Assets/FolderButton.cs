using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderButton : MonoBehaviour {
    public GameObject folder;
    bool opened=true;
	public void CloseFolder()
    {
        if (opened == false)
        {
            OpenFolder();
        }
        else
        {
            folder.GetComponent<Animation>()["FolderAnim"].speed = 1;
            folder.GetComponent<Animation>()["FolderAnim"].time = 0;
            folder.GetComponent<Animation>().Play("FolderAnim");
            opened = false;
        }
    }
    public void OpenFolder()
    {
        opened = true;
        folder.GetComponent<Animation>()["FolderAnim"].speed = -1;
        folder.GetComponent<Animation>()["FolderAnim"].time = folder.GetComponent<Animation>()["FolderAnim"].length;
        folder.GetComponent<Animation>().Play("FolderAnim");
    }
}
