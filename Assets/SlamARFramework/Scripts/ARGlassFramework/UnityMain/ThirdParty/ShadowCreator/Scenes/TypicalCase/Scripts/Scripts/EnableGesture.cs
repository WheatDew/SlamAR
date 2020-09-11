using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;

public class EnableGesture : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        API_InputSystem.EnableInputDeivce(InputDeviceType.Gesture26DofHand);
    }
}
