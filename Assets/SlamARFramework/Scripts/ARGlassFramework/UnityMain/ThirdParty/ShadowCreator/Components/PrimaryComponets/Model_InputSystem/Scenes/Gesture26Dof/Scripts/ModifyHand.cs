using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHand : MonoBehaviour {

    public InputDevicePartType partType;

    Vector3 offset = new Vector3(0f, 0f, 0f);


    bool displayHandLine = true;
    public void HandLine() {
        displayHandLine = !displayHandLine;

        if(InputSystem.Instant.Gesture26Dof) {
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    part.DisplayFingerLineRender(displayHandLine);
                }
            }
        }
    }
    bool displayJoint = false;
    public void Joint() {

        displayJoint = !displayJoint;

        if(InputSystem.Instant.Gesture26Dof) {
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    part.DisplayFingerDetecter(displayJoint);
                }
            }
        }
    }

    bool turnOffRay = true;
    public void Ray() {

        turnOffRay = !turnOffRay;

        if(InputSystem.Instant.Gesture26Dof) {
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    part.SetRay(turnOffRay);
                }
            }
        }
    }

    bool turnOffTouch = true;
    public void Touch() {
        
        turnOffTouch = !turnOffTouch;

        if(InputSystem.Instant.Gesture26Dof) {
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    part.SetTouch(turnOffTouch);
                }
            }
        }
    }

}
