using SC.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PointerSlowDrag : PointerHandlers {
    public override void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData) {
        transform.position = eventData.dragSlowPosition;
    }

}
