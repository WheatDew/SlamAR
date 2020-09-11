using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 前置资源加载
/// </summary>
public class DownloadAllPrepositionAssets : MonoSingleton<DownloadAllPrepositionAssets>
{
    public bool hasDownJsonData;

    public bool hasDownJsonAssets;

    public bool hasDownVuforiaData;

    public bool hasDownHotfixData;

    public bool hasDownVuforia;

    public bool hasInitAddressables;

    public bool HasDownJsonAssets()
    {
        return hasDownJsonAssets;
    }

    public bool HasDownJsonData()
    {
        return hasDownJsonData;
    }

    public bool HasDownVuforiaData()
    {
        return hasDownVuforiaData;
    }

    public bool HasDownVuforia()
    {
        return hasDownVuforia;
    }

    public bool HasDownHotfixData()
    {
        
        return hasDownHotfixData;
    }
//#if UNITY_EDITOR
//    private void Update()
//    {
//        if (HasDownHotfixData())
//            GameObject.Find("Canvas/2").GetComponent<Text>().text = "热更新资源已经下载完毕";
//        if (HasDownJsonData())
//            GameObject.Find("Canvas/1").GetComponent<Text>().text = "Json数据已经下载完毕";
//        if (HasDownVuforiaData())
//            GameObject.Find("Canvas/3").GetComponent<Text>().text = "Vuforia数据已经下载完毕";
//        if (HasDownVuforia())
//            GameObject.Find("Canvas/4").GetComponent<Text>().text = "Vuforia已经下载完毕";
//        if (HasInitAssets())
//            GameObject.Find("Canvas/5").GetComponent<Text>().text = "资源初始化完成";
//    }
//#endif
}
