using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioChanelSlot : Slot, IPointerClickHandler
{
    PlayerController pController;
    public int TrackID = 0;
    private void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    public override void OnDrop(PointerEventData eventData)
    {
       
        if (item)
        {

            pController.RemoveTrack(TrackID);
            item.GetComponent<DragTrackHandler>().inAudioChanel = false;
            item.transform.position = item.GetComponent<DragTrackHandler>().startPosition;
            item.transform.parent = item.GetComponent<DragTrackHandler>().startParent;
        }
        pController.PutTrack(DragTrackHandler.trackDragged.GetComponent<DragTrackHandler>().sample, TrackID);
        base.OnDrop(eventData);
        DragTrackHandler.trackDragged.transform.SetParent(transform);
        DragTrackHandler.trackDragged.GetComponent<DragTrackHandler>().inAudioChanel = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item)
        {
            DragTrackHandler drag = item.GetComponent<DragTrackHandler>();
            pController.RemoveTrack(TrackID);
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
            item.transform.position = drag.startPosition;
            drag.inAudioChanel = false;
            item.transform.SetParent(drag.startParent);
        }
    }
}
