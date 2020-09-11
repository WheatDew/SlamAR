using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem.InputDeviceHandShank;
using SC.InputSystem;

public class HandShankTouchEvent : HandShankEventDelegate {

    public InputDevicePartType partType;

	public MeshRenderer left;
	public MeshRenderer right;
	public MeshRenderer up;
	public MeshRenderer down;

    protected override void handShankDelegate(HandShankEvent aEvent, InputDeviceHandShankPart handShankPart) {
        base.handShankDelegate(aEvent, handShankPart);

        Debug.Log("HandShankTouchEvent  Part:"+ handShankPart.PartType+"  Event:"+ aEvent);

        if(partType == handShankPart.PartType && aEvent == HandShankEvent.TouchUp) {
            left.materials[0].color = Color.white;
            right.materials[0].color = Color.white;
            down.materials[0].color = Color.white;
            up.materials[0].color = Color.white;

        } else if(partType == handShankPart.PartType && aEvent == HandShankEvent.TouchSlideDown) {
            down.materials[0].color = Color.blue;

        } else if(partType == handShankPart.PartType && aEvent == HandShankEvent.TouchSlideLeft) {
            left.materials[0].color = Color.blue;

        } else if(partType == handShankPart.PartType && aEvent == HandShankEvent.TouchSlideRight) {
            right.materials[0].color = Color.blue;

        } else if(partType == handShankPart.PartType && aEvent == HandShankEvent.TouchSlideUp) {
            up.materials[0].color = Color.blue;

        }
    }


}
