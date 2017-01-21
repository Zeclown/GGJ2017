using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour,IDropHandler {
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            return null;
        }
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if(item)
        {
            item.transform.position = item.GetComponent<DragTrackHandler>().startPosition;
            item.transform.parent=item.GetComponent<DragTrackHandler>().startParent;

        }
        DragTrackHandler.trackDragged.transform.SetParent(transform);
        DragTrackHandler.trackDragged.GetComponent<DragTrackHandler>().inAudioChanel = false;
    }
}
