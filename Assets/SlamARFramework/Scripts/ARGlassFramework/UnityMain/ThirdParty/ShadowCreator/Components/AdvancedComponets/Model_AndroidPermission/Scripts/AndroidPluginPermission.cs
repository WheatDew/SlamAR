using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WCQ.CommonV1_20200325;
using UnityEngine.Android;

public class AndroidPluginPermission : AndroidPluginBase
{
    private static AndroidPluginPermission Instant;
    public static AndroidPluginPermission getInstant {
        get {
            if(Instant == null) {
                Instant = new AndroidPluginPermission();
            }
            return Instant;
        }
    }
    public AndroidJavaClass AndroidPermissionClass { get; private set; }
    public AndroidJavaObject AndroidPermissionObject { get; private set; }
    private AndroidPluginPermission() {
        AndroidPermissionClass = GetAndroidJavaClass("com.example.libpermission.PermissionRequest");
        AndroidPermissionObject = ClassFunctionCallStatic<AndroidJavaObject>(AndroidPermissionClass, "getInstant", CurrentActivity);

    }

    public void RequestPermission(string[] permissionList) {
        ObjectFunctionCall(AndroidPermissionObject, "RequestPermission", permissionList);
    }
}
