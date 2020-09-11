using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventID
{
    None = 0,
    UICanvasWithHeadCamera,
    LoadTargetHotfixAssets,
    SetAssetsParent,
    SetAssetsChildParent,
    SetAssetsParentPosition,
    SetAssetsParentRotate,
    SetAssetsChildPosition,
    SetAssetsChildRotate,
    SetAssetsVuforiaChild,
}

public enum HandleType
{
    Add = 0,
    Remove = 1,
}

public class ClientEventSystemDefine
{
    public static Dictionary<int, string> dicHandleType = new Dictionary<int, string> {
        { (int)HandleType.Add,"Add"},
        { (int)HandleType.Remove,"Remove"},
    };
}
