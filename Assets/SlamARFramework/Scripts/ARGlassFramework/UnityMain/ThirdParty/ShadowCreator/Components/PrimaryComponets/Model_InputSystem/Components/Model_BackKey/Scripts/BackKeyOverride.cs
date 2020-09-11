using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class BackKeyOverride : PointerDelegate
{
    public static BackKeyOverride Instant;
    public UnityEvent BackKeyCallBack = new UnityEvent();

    void Awake() {
        if(Instant) {
            DestroyImmediate(gameObject);
            return;
        }
        Instant = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void partAnyKeyDownDelegate(InputKeyCode keyCode, InputDevicePartBase part) {
        base.partAnyKeyDownDelegate(keyCode, part);

        if(keyCode != InputKeyCode.Back)
            return;

        if(BackKeyCallBack != null) {
            BackKeyCallBack.Invoke();
        }
    }

    void Update() {
        if(BackKeyCallBack != null) {
            Input.backButtonLeavesApp = true;
        } else {
            Input.backButtonLeavesApp = false;
        }

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Escape)) {
            partAnyKeyDownDelegate(InputKeyCode.Back, null);
        }
#endif

    }

    void OnDestroy() {
        if(BackKeyCallBack != null) {
            BackKeyCallBack.RemoveAllListeners();
        }
    }
}
