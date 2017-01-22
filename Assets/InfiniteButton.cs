using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteButton : MonoBehaviour {

	public void PlayInfinite()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
