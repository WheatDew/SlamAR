using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using System;
public class AnykeyDelegate : PointerDelegate
{
    protected override void partAnyKeyDownDelegate(InputKeyCode keyCode, InputDevicePartBase part) {
        base.partAnyKeyDownDelegate(keyCode, part);
        try {
            GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        } catch(Exception e) {
            Debug.Log(e);
        }
    }
}
