using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundboardUI : MonoBehaviour {
    bool folkUnlocked=false;
    bool metalUnlocked=false;
    FolderButton folder;
    private void Start()
    {
        folder = FindObjectOfType<FolderButton>();
    }
    public void UnlockFolk()
    {
        if (!folkUnlocked)
            StartCoroutine("FolkUnlockAnim");
        folkUnlocked = true;
    }
    public void UnlockMetal()
    {
        if (!metalUnlocked)
            StartCoroutine("MetalUnlockAnim");
        metalUnlocked = true;
    }
    IEnumerator FolkUnlockAnim()
    {
        if(!folder.opened)
            folder.OpenFolder();
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animation>().Play("UnlockFolk");
    }
    IEnumerator MetalUnlockAnim()
    {
        if (!folder.opened)
            folder.OpenFolder();
        yield return new WaitForSeconds(1.5f);
        GetComponent<Animation>().Play("UnlockMetal");
    }
}
