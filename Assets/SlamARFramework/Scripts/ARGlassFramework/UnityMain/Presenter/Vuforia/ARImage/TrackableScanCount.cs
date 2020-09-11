using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrackableScanCount : MonoSingleton<TrackableScanCount>
{
    JsonWriter data = new JsonWriter();
    private void OnEnable()
    {
        data.WriteArrayStart();        
    }

    public void WhiteJson(string name, string time)
    {
        data.WriteObjectStart();
        data.WritePropertyName(name);
        data.Write(time);
        data.WriteObjectEnd();
        //Debug.Log(data.ToString());
    }

    public void EndJson()
    {
        data.WriteArrayEnd();
        StreamWriter sw = new StreamWriter(PathSet.GetVuforiaTrackableScanTimeSavePath());
        //FileInfo fi = new FileInfo(PathSet.GetVuforiaTrackableScanTimeSavePath());  
        //sw = fi.AppendText();//接着往下写
        //sw = fi.CreateText(); //重新写入
        sw.Write(data.ToString());
        sw.Close();
        //sw.Dispose();
    }

    private void OnDisable()
    {
        EndJson();
    }
}
