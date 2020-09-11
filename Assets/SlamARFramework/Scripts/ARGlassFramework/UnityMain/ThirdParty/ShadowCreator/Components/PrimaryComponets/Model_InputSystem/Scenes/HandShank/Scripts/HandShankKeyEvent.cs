using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem.InputDeviceHandShank;
using SC.InputSystem;

public class HandShankKeyEvent : HandShankKeyHandlers {

    public override void OnFunctionKeyDown(InputDeviceHandShankPart part, SCPointEventData eventData) {
        base.OnFunctionKeyDown(part, eventData);
        GetComponent<MeshRenderer>().material.color = Color.blue;

    }

    public override void OnFunctionKeyUp(InputDeviceHandShankPart part, SCPointEventData eventData) {
        base.OnFunctionKeyUp(part, eventData);
        GetComponent<MeshRenderer>().material.color = Color.gray;
    }

}

