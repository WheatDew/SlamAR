using System.Collections;
using System.Collections.Generic;
using SC.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTest : DragComponent
{
    public override void HideBoxEdit()
    {
        base.HideBoxEdit();
    }
    public override void ShowBoxEdit()
    {
        base.ShowBoxEdit();
    }
    public override void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerDown(part, eventData);
    }
    public override void OnSCPointerUp(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerUp(part, eventData);
    }

    public override void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerDrag(part, eventData);
        changeLine();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void InitEulerManager()
    {
        base.InitEulerManager();
    }

    public override void InitScaleManager()
    {
        base.InitScaleManager();
    

    }

}
