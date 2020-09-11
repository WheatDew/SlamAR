using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using UnityEngine.UI;
public class ControlHandShank : MonoBehaviour
{
    bool isHandShank1Enable {
        get {
            if(InputSystem.Instant.HandShank) {
                return InputSystem.Instant.HandShank.OneHandShankOpen;
            }
            return false;
        }
        set {
            if(InputSystem.Instant.HandShank) {
                InputSystem.Instant.HandShank.OneHandShankOpen = value;
            }
        }
    }
    bool isHandShank2Enable {
        get {
            if(InputSystem.Instant.HandShank) {
                return InputSystem.Instant.HandShank.TwoHandShankOpen;
            }
            return false;
        }
        set {
            if(InputSystem.Instant.HandShank) {
                InputSystem.Instant.HandShank.TwoHandShankOpen = value;
            }
        }
    }

    public Text handShank1Text, handShank2Text;

    void Start() {
        //InputSystem.Instant.RegisterInputDevice(InputDeviceType.HandShank);
        //InputSystem.Instant.RegisterInputDevice(InputDeviceType.Head);
        //InputSystem.Instant.UnRegisterInputDevice(InputDeviceType.Gesture26DofHand);
    }
    public void SwitchHandShank1() {
        isHandShank1Enable = !isHandShank1Enable;

        if(! isHandShank1Enable) {
            handShank1Text.text = "Enable HandShank1";
        } else {
            handShank1Text.text = "Disable HandShank1";
        }
    }
    public void SwitchHandShank2() {
        isHandShank2Enable = !isHandShank2Enable;

        if(! isHandShank2Enable) {
            handShank2Text.text = "Enable HandShank2";
        } else {
            handShank2Text.text = "Disable HandShank2";
        }
    }
}
