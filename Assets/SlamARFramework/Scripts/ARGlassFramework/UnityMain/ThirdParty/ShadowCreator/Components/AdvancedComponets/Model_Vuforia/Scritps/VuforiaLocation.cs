using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC;

public class VuforiaLocation : MonoBehaviour {
    public Vector3 RGBPositionOffset = new Vector3(0,0.07f,-0.03f);
    public Vector3 RGBRotationOffset;
    // Use this for initialization
    void Start() {
        StartCoroutine(InitARCamera());
    }

    IEnumerator InitARCamera() {
        while(true) {
            if(SvrManager.Instance.IsRunning) {
                transform.SetParent(SvrManager.Instance.head, false);
                if(ShadowSystem.Instant != null) {
                    if(ShadowSystem.Instant.Device) {
                        transform.localEulerAngles = - ShadowSystem.Instant.Device.CurrentDevice.RGBRotationOffset + RGBRotationOffset;
                        transform.localPosition = - ShadowSystem.Instant.Device.CurrentDevice.RGBPositionOffset + RGBPositionOffset;
                    } else {
                        transform.localEulerAngles = RGBRotationOffset;
                        transform.localPosition = RGBPositionOffset;
                    }
                }
                    
                yield break;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update() {
        //gameObject.transform.position = SvrManager.Instance.modifyPosition;
        //gameObject.transform.rotation = SvrManager.Instance.modifyOrientation;
    }
}
