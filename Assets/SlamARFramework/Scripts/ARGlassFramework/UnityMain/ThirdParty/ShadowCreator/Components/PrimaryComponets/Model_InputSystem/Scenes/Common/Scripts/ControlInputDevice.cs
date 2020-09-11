using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using UnityEngine.UI;
public class ControlInputDevice : MonoBehaviour
{
    bool isHeadEnable {
        get {
            return InputSystem.Instant.Head != null? true:false;
        }
    }
    bool isHandShankEnable {
        get {
            return InputSystem.Instant.HandShank != null ? true : false;
        }
    }
    bool isGestureEnable {
        get {
            return InputSystem.Instant.Gesture26Dof != null ? true : false;
        }
    }

    public Text headText,handShankText,GestureText;

    void Start() {
        //InputSystem.Instant.RegisterInputDevice(InputDeviceType.Head);
        //InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.HandShank);
        //InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.Gesture26DofHand);
    }
    public void SwitchHead() {

        if(isHeadEnable) {
            InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.Head);
            headText.text = "Enable Head";
        } else{
            InputSystem.Instant.RegisterInputDevice(InputDeviceType.Head);
            headText.text = "Disable Head";
        }
    }

    public void SwitchHandShank() {

        if(isHandShankEnable) {
            handShankText.text = "Enable HandShank";
            InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.HandShank);
        } else {
            handShankText.text = "Disable HandShank";
            InputSystem.Instant.RegisterInputDevice(InputDeviceType.HandShank);
        }
    }

    public void SwitchGesture() {

        if(isGestureEnable) {
            GestureText.text = "Enable Gesture";
            InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.Gesture26DofHand);
        } else {
            GestureText.text = "Disable Gesture";
            InputSystem.Instant.RegisterInputDevice(InputDeviceType.Gesture26DofHand);
        }
    }
}
