using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using UnityEngine.UI;
public class ControlGesture26Dof : MonoBehaviour
{
    bool isHandLeftEnable {
        get {
            if(InputSystem.Instant.Gesture26Dof) {
                return InputSystem.Instant.Gesture26Dof.LeftHandOpen;
            }
            return false;
        }
        set {
            if(InputSystem.Instant.Gesture26Dof) {
                InputSystem.Instant.Gesture26Dof.LeftHandOpen = value;
            }
        }
    }
    bool isHandRightEnable {
        get {
            if(InputSystem.Instant.Gesture26Dof) {
                return InputSystem.Instant.Gesture26Dof.RightHandOpen;
            }
            return false;
        }
        set {
            if(InputSystem.Instant.Gesture26Dof) {
                InputSystem.Instant.Gesture26Dof.RightHandOpen = value;
            }
        }
    }

    public Text handLeftText, handRightText;

    void Start() {
        //InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.HandShank);
        //InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.Head);
        InputSystem.Instant.RegisterInputDevice(InputDeviceType.Gesture26DofHand);
    }
    public void SwitchHandLeft() {
        isHandLeftEnable = !isHandLeftEnable;

        if(!isHandLeftEnable) {
            handLeftText.text = "Enable LeftHand";
        } else {
            handLeftText.text = "Disable LeftHand";
        }
    }
    public void SwitchHandRight() {
        isHandRightEnable = !isHandRightEnable;

        if(!isHandRightEnable) {
            handRightText.text = "Enable RightHand";
        } else {
            handRightText.text = "Disable RightHand";
        }
    }
}
