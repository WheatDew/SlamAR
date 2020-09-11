using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;

public class HandShank3Dof : MonoBehaviour {
    public int deviceId;
    public TextMesh stateText;

    public InputDevicePartType partType;

    // Update is called once per frame
    void LateUpdate() {
        if(InputSystem.Instant.HandShank != null) {

            stateText.text = "未连接";
            foreach(var part in InputSystem.Instant.HandShank.inputDeviceHandShankPartList) {
                if(part.PartType == partType) {
                    if(part.inputDataHandShank.isVaild) {
                        stateText.text = "已连接";
                        transform.rotation = part.inputDataHandShank.rotation;
                    }
                }
            }
            
        }
    }
}

