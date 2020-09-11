using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackKeyDialogUI : MonoBehaviour
{
    Vector3 offset = new Vector3(0,0,1.2f);
    int cullingMask;
    void Awake() {
        if(SvrManager.Instance != null && SvrManager.Instance.head != null) {
            cullingMask = SvrManager.Instance.leftCamera.cullingMask;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (SvrManager.Instance != null && SvrManager.Instance.head != null)  {
            transform.position = SvrManager.Instance.head.TransformPoint(offset);
            transform.eulerAngles = SvrManager.Instance.head.eulerAngles;
            SvrManager.Instance.leftCamera.cullingMask = 0;
            SvrManager.Instance.rightCamera.cullingMask = 0;
        }
    }

    void OnDisable() {
        if(SvrManager.Instance != null && SvrManager.Instance.head != null) {
            SvrManager.Instance.leftCamera.cullingMask = cullingMask;
            SvrManager.Instance.rightCamera.cullingMask = cullingMask;
        }
    }

    public void PressEnter() {
        Application.Quit();
    }

    public void PressCancel() {
        gameObject.SetActive(false);
    }
}
