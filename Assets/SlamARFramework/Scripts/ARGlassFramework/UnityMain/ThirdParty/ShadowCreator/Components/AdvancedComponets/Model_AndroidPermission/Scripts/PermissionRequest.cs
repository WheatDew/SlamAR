using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionRequest : MonoBehaviour
{
    string[] permissionArray;

    public bool Camera = true;
    public bool ExternalStorageRead = true;
    public bool ExternalStorageWrite = true;
    public bool Microphone = false;
    public bool FineLocation = false;
    public bool CoarseLocation = false;

    void Awake() {
        List<string> permissionList = new List<string>();

        if(Camera) {
            permissionList.Add(Permission.Camera);
        }
        if(ExternalStorageRead) {
            permissionList.Add(Permission.ExternalStorageRead);
        }
        if(ExternalStorageWrite) {
            permissionList.Add(Permission.ExternalStorageWrite);
        }
        if(Microphone) {
            permissionList.Add(Permission.Microphone);
        }
        if(FineLocation) {
            permissionList.Add(Permission.FineLocation);
        }
        if(CoarseLocation) {
            permissionList.Add(Permission.CoarseLocation);
        }

        permissionArray = permissionList.ToArray();

        if(permissionArray.Length > 0) {
            AndroidPluginPermission.getInstant.RequestPermission(permissionArray);
        }
    }



}
