using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionModify : MonoBehaviour {

    public InputDevicePartType partType;

    Vector3 offset = new Vector3(0f, 0f, 0f);
    float fv = 0.01f;//每次调节1cm
    void Start() {
        StartCoroutine(CheckGesture26Dof());
    }

    IEnumerator CheckGesture26Dof() {
        while(true) {

            yield return null;
            yield return new WaitUntil(() => InputSystem.Instant.Gesture26Dof == true);

            ///调节手的位置
            foreach(var part in InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList) {
                if(part.PartType == partType) {
                    part.HandPositionModify(offset);
                }
            }
        }
    }

    public void Up() {
        offset += Vector3.up * fv;
    }
    public void Down() {
        offset += Vector3.down * fv;
    }
    public void Left() {
        offset += Vector3.left * fv;
    }
    public void Right() {
        offset += Vector3.right * fv;
    }
    public void Forward() {
        offset += Vector3.forward * fv;
    }
    public void Back() {
        offset += Vector3.back * fv;
    }
    void Update() {

        ///获取手掌中心位置
        //Debug.Log("HandCenterPosition:" + InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList[0].inputDevice26DofGestureUI.model26DofGesture.center.transform.position);

        ///获取关节点的位置
        //Debug.Log(InputSystem.Instant.Gesture26Dof.InputDevice26DofGesturePartList[0].GetJointPosition(true, SC.InputSystem.InputDevice26DofGesture.FINGER.forefinger, SC.InputSystem.InputDevice26DofGesture.JOINT.One));
    }

}
