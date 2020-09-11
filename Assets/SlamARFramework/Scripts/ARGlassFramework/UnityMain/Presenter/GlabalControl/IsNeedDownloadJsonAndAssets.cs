using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNeedDownloadJsonAndAssets : MonoSingleton<IsNeedDownloadJsonAndAssets>
{
    [SerializeField]
    bool hasGetMsgFromServer;
    [SerializeField]
    bool isNeedDownload;

    public bool IsNeed { get => isNeedDownload; set => isNeedDownload = value; }
    public bool HasGetMsgFromServer1 { get => hasGetMsgFromServer; set => hasGetMsgFromServer = value; }

    public bool IsNeedDownload()
    {
        return IsNeed;
    }

    public bool HasGetMsgFromServer()
    {
        return HasGetMsgFromServer1;
    }
}
