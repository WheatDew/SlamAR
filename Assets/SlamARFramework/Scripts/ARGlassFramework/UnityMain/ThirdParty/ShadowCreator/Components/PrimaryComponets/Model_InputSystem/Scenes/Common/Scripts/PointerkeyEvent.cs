using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using System;
public class PointerkeyEvent : PointerHandlers
{
    public override void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData) {
        base.OnSCPointerDown(part, eventData);
        try {
            GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        } catch(Exception e) {
            Debug.Log(e);
        }
    }
}
