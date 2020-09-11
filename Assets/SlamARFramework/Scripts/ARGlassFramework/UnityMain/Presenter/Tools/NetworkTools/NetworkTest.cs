using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NetworkTest : MonoBehaviour
{
    public VideoPlayer player;
    public string videoName;
    string savePath ;
    string readPath;
    void Start()
    {
        savePath = Application.persistentDataPath + "/"+ videoName;
        readPath = "file://" + Application.persistentDataPath + "/"+ videoName;
        NetworkTools.GetInstance.Get("http://117.81.233.142:8081/Test/"+ videoName, t =>
        {
            try
            {
                if (!Directory.Exists(Application.persistentDataPath))
                {
                    Directory.CreateDirectory(Application.persistentDataPath);
                }
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
                fs.Write(t.downloadHandler.data, 0, t.downloadHandler.data.Length);
                fs.Close();
                player.source = VideoSource.Url;
                player.url = readPath;
                player.Play();
            }
            catch(Exception ex) { Debug.LogError(ex);}
        },()=> {
            Debug.Log("当前网络不好");
            if (!System.IO.File.Exists(readPath))
            {
                print("文件不存在");
            }
            else
            {
                print("文件存在");
            }
        });
    }
}
