using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHandPos : MonoBehaviour {

    public InputDevicePartType partType;

    public TextMesh text;
    void LateUpdate() {

        if(InputSystem.Instant.Gesture26Dof) {
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    text.text = "FINGERsmall-JOINTfive\nworldPos:" + part.GetJointPosition(true, SC.InputSystem.InputDevice26DofGesture.FINGER.small, SC.InputSystem.InputDevice26DofGesture.JOINT.Five).ToString("(0.00,0.00,0.00)");
                    text.text +="\nlocalPos:"+ part.GetJointPosition(false, SC.InputSystem.InputDevice26DofGesture.FINGER.forefinger, SC.InputSystem.InputDevice26DofGesture.JOINT.One).ToString("(0.00,0.00,0.00)");
                }
            }
        }
    }

}
