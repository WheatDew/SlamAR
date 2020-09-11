using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSet 
{
    #region Vuforia

    /// <summary>
    /// Vuforia的Xml读取路径（持久化路径），注意，用Vuforia自己的API读取，无需加上"file://"，Playersetting中公司和App名字不能用中文
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaReadXmlPath()
    {
        string path;
        if (Application.isEditor) path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        else if(Application.platform == RuntimePlatform.Android) path =  Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        else path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        return path;
    }

    /// <summary>
    /// Vuforia的Xml存储路径（持久化路径）
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaSaveXmlPath()
    {
        string path;
        if (Application.isEditor) path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        else if (Application.platform == RuntimePlatform.Android) path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        else path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.xml";
        return path;
    }

    /// <summary>
    /// Vuforia的Xml存储路径（持久化路径）
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaSaveDatPath()
    {
        string path;
        if (Application.isEditor) path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.dat";
        else if (Application.platform == RuntimePlatform.Android) path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.dat";
        else path = Application.persistentDataPath + "/Vuforia/VuforiaHotfix.dat";
        return path;
    }

    #endregion

    #region Json

    /// <summary>
    /// Json存储路径（持久化目录）
    /// </summary>
    /// <returns></returns>
    public static string GetJsonSavePath()
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/xinyancompany/Company333.txt";
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/xinyancompany/Company333.txt";
        else saveDatPath = Application.persistentDataPath + "/xinyancompany/Company333.txt";
        return saveDatPath;
    }

    /// <summary>
    /// Json读取路径（持久化目录）
    /// </summary>
    /// <returns></returns>
    public static string GetJsonReadPath()
    {
        string readDatPath;
        if (Application.isEditor) readDatPath = Application.persistentDataPath + "/xinyancompany/Company333.txt";
        else if (Application.platform == RuntimePlatform.Android) readDatPath = "file://" + Application.persistentDataPath + "/xinyancompany/Company333.txt";
        else readDatPath = "file://" + Application.persistentDataPath + "/xinyancompany/Company333.txt";
        return readDatPath;
    }

    /// <summary>
    /// 图片存储路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetImageSavePath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/" + name;
        else saveDatPath = Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 图片读取路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetImageReadPath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        else saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 音频存储路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetAudioSavePath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/" + name;
        else saveDatPath = Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 音频读取路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetAudioReadPath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        else saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 视频读取路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetVideoReadPath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        else saveDatPath = "file://" + Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 视频存储路径（持久化目录）
    /// </summary>
    /// <param name="name">json对应的文件名</param>
    /// <returns></returns>
    public static string GetVideoSavePath(string name)
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/" + name;
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/" + name;
        else saveDatPath = Application.persistentDataPath + "/" + name;
        return saveDatPath;
    }

    /// <summary>
    /// 扫描到识别图后记录并写入
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaTrackableScanTimeSavePath()
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/VuforiaTrackable.txt";
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/VuforiaTrackable.txt";
        else saveDatPath = Application.persistentDataPath + "/VuforiaTrackable.txt";
        return saveDatPath;
    }

    /// <summary>
    /// 扫描到识别图后记录并写入
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaTrackableScanTimeReadPath()
    {
        string saveDatPath;
        if (Application.isEditor) saveDatPath = Application.persistentDataPath + "/VuforiaTrackable.txt";
        else if (Application.platform == RuntimePlatform.Android) saveDatPath = Application.persistentDataPath + "/VuforiaTrackable.txt";
        else saveDatPath = "file://" + Application.persistentDataPath + "/VuforiaTrackable.txt";
        return saveDatPath;
    }
    #endregion

    /// <summary>
    /// 存于服务器上的Json路径
    /// </summary>
    /// <returns></returns>
    public static string GetJsonDownPath()
    { 
        return "http://117.81.233.142:8081/MRGlass_Shadows/xinyancompany/Company333.txt";
    }

    /// <summary>
    /// 存于服务器上的Vuforia的Xml路径
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaXmlPath()
    {
        return "http://117.81.233.142:8081/MRGlass_Shadows/Vuforia/VuforiaHotfix.xml";
    }

    /// <summary>
    /// 存于服务器上的Vuforia的Dat路径
    /// </summary>
    /// <returns></returns>
    public static string GetVuforiaDatPath()
    {
        return "http://117.81.233.142:8081/MRGlass_Shadows/Vuforia/VuforiaHotfix.dat";
    }
}
