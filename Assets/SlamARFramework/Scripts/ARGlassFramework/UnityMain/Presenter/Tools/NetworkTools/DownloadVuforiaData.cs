using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DownloadVuforiaData : MonoBehaviour
{
    string saveXMLPath;
    string saveDatPath;
    bool hasXML;
    bool hasDat;
    IEnumerator Start()
    {
        StartCoroutine(HasDownVuforiaData());
        saveXMLPath = PathSet.GetVuforiaSaveXmlPath();
        saveDatPath = PathSet.GetVuforiaSaveDatPath();
        yield return new WaitUntil(IsNeedDownloadJsonAndAssets.GetInstance.HasGetMsgFromServer);
        //获取网络
        if (IsNeedDownloadJsonAndAssets.GetInstance.IsNeedDownload())
        {
            NetworkTools.GetInstance.Get(PathSet.GetVuforiaXmlPath(), t =>
            {
                try
                {
                    if (!Directory.Exists(Application.persistentDataPath + "/Vuforia/"))
                    {
                        Directory.CreateDirectory(Application.persistentDataPath + "/Vuforia/");
                    }
                    FileStream fs = new FileStream(saveXMLPath, FileMode.Create, FileAccess.ReadWrite);
                    fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                    fs.Close();
                    hasXML = true;
                }
                catch (Exception ex) { Debug.LogError(ex); }
            }, () => { });

            yield return new WaitUntil(() => { return hasXML; });

            NetworkTools.GetInstance.Get(PathSet.GetVuforiaDatPath(), t =>
            {
                try
                {
                    if (!Directory.Exists(Application.persistentDataPath + "/Vuforia/"))
                    {
                        Directory.CreateDirectory(Application.persistentDataPath + "/Vuforia/");
                    }
                    FileStream fs = new FileStream(saveDatPath, FileMode.Create, FileAccess.ReadWrite);
                    fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                    fs.Close();
                    hasDat = true;
                }
                catch (Exception ex) { Debug.LogError(ex); }
            }, () => { });
        }
        else
        {
            DownloadAllPrepositionAssets.GetInstance.hasDownVuforiaData = true;
        }
    }

    bool HasDownAll()
    {
        return hasXML && hasDat;
    }

    IEnumerator HasDownVuforiaData()
    {
        yield return new WaitUntil(HasDownAll);
        DownloadAllPrepositionAssets.GetInstance.hasDownVuforiaData = true;
    }
}
