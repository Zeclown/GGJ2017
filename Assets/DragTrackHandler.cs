using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragTrackHandler : MonoBehaviour , IBeginDragHandler,IDragHandler,IEndDragHandler{
    public static GameObject trackDragged;
    public Vector3 startPosition;
    public Transform startParent;
    public MusicSample sample;
    [HideInInspector]
    public bool inAudioChanel=false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        trackDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!inAudioChanel)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!inAudioChanel)
        {
            trackDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent == startParent || transform.parent.GetComponent<AudioChanelSlot>() ==null)
            {
                transform.parent = startParent;
                transform.position = startPosition;
            }
            
        }
       
    }

    
}
