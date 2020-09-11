using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
///UnityWebRequest下载上传资源管理器
/// </summary>
public class NetworkTools : MonoSingleton<NetworkTools>
{
    UnityWebRequest uwr;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text text;
    float preProcess;
    //[SerializeField]
    float time;
    [SerializeField]
    bool hasDown;
    //[SerializeField]
    float missTime;
    [SerializeField]
    bool missNet;
    new void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (hasDown) return;
        if (uwr != null && !uwr.isDone)
        {
            time = 0;
            slider.value = uwr.downloadProgress;
            preProcess = slider.value;
        }
        else if (DownloadAllPrepositionAssets.GetInstance.HasDownJsonAssets() && slider.value > 0.9f && uwr == null)
        {
            time += Time.deltaTime;
            if (time > 10)
            {
                hasDown = true;
                Debug.Log("所有资源加载完成");
            }
        }
        else if (uwr != null && preProcess == slider.value)
        {
            missTime += Time.deltaTime;
            if (missTime > 60)
            {
                missNet = true;
                Debug.Log("网络连接失败");
            }
        }
    }


    #region 公开的方法
    public bool HasDownAllAssets()
    {
        return hasDown;
    }

    public bool HasMissNet()
    {
        return missNet;
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="action"></param>
    public void Get(string url, Action<UnityWebRequest> actionResult, Action errorAction)
    {
        StartCoroutine(_Get(url, actionResult, errorAction));
    }

    /// <summary>
    /// 下载文字
    /// </summary>
    /// <param name="url"></param>
    /// <param name="actionResult"></param>
    public void DownloadText(string url, Action<string> actionResult)
    {
        StartCoroutine(_GetText(url, actionResult));
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="downloadFilePathAndName">储存文件的路径和文件名 like 'Application.persistentDataPath+"/unity3d.html"'</param>
    /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求对象</param>
    /// <returns></returns>
    public void DownloadFile(string url, string downloadFilePathAndName, Action<UnityWebRequest> actionResult)
    {
        StartCoroutine(_DownloadFile(url, downloadFilePathAndName, actionResult));
    }

    /// <summary>
    /// 请求图片
    /// </summary>
    /// <param name="url">图片地址</param>
    /// <param name="action">请求发起后处理回调结果的委托,处理请求结果的图片</param>
    /// <returns></returns>
    public void DownloadTexture(string url, Action<Texture2D> actionResult, Action errorAction)
    {
        StartCoroutine(_GetTexture(url, actionResult, errorAction));
    }

    /// <summary>
    /// 请求服务器地址上的音效
    /// </summary>
    /// <param name="url">没有音效地址</param>
    /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求结果的AudioClip</param>
    /// <param name="audioType">音效类型</param>
    /// <returns></returns>
    public void DownloadAudioClip(string url, Action<AudioClip> actionResult, Action errorAction, AudioType audioType = AudioType.MPEG)
    {
        StartCoroutine(_GetAudioClip(url, actionResult, errorAction, audioType));
    }


    /// <summary>
    /// 向服务器提交post请求
    /// </summary>
    /// <param name="serverURL">服务器请求目标地址,like "http://www.shijing720.com/myform"</param>
    /// <param name="lstformData">form表单参数</param>
    /// <param name="lstformData">处理返回结果的委托,处理请求对象</param>
    /// <returns></returns>
    public void Post(string serverURL, List<IMultipartFormSection> lstformData, Action<UnityWebRequest> actionResult)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        StartCoroutine(_Post(serverURL, lstformData, actionResult));
    }

    public void Post(string serverURL, WWWForm form, Action<UnityWebRequest> actionResult)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        StartCoroutine(_Post(serverURL, form, actionResult));
    }

    /// <summary>
    /// 通过PUT方式将字节流传到服务器
    /// </summary>
    /// <param name="url">服务器目标地址 like 'http://www.shijing720.com/upload' </param>
    /// <param name="contentBytes">需要上传的字节流</param>
    /// <param name="resultAction">处理返回结果的委托</param>
    /// <returns></returns>
    public void UploadByPut(string url, byte[] contentBytes, Action<bool> actionResult)
    {
        StartCoroutine(_UploadByPut(url, contentBytes, actionResult, ""));
    }


    #endregion

    #region 私有的方法
    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="action">请求发起后处理回调结果的委托</param>
    /// <returns></returns>
    IEnumerator _Get(string url, Action<UnityWebRequest> actionResult, Action errorAction)
    {
        yield return new WaitUntil(() => { return uwr == null; });
        uwr = UnityWebRequest.Get(url);
        {
            yield return uwr.SendWebRequest();
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                if (actionResult != null)
                {
                    actionResult(uwr);
                    uwr = null;
                }
            }
            else
            {
                if (errorAction != null)
                    errorAction.Invoke();
                Debug.LogError("下载失败，请检查网络，或者下载地址是否正确: " + url);
            }
        }
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="downloadFilePathAndName">储存文件的路径和文件名 like 'Application.persistentDataPath+"/unity3d.html"'</param>
    /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求对象</param>
    /// <returns></returns>
    IEnumerator _DownloadFile(string url, string downloadFilePathAndName, Action<UnityWebRequest> actionResult)
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        uwr.downloadHandler = new DownloadHandlerFile(downloadFilePathAndName);
        yield return uwr.SendWebRequest();

        if (!(uwr.isNetworkError || uwr.isHttpError))
        {
            if (actionResult != null)
            {
                actionResult(uwr);
            }
        }
        else
        {
            Debug.LogError("下载失败，请检查网络，或者下载地址是否正确: " + url);
        }
    }

    /// <summary>
    /// 下载文字
    /// </summary>
    /// <param name="url"请求地址></param>
    /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求对象</param>
    /// <returns></returns>
    IEnumerator _GetText(string url, Action<string> actionResult)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            if (!(uwr.isHttpError || uwr.isNetworkError))
            {
                if (actionResult != null)
                    actionResult(uwr.downloadHandler.text);
            }
            else
            {
                Debug.LogError("下载失败，请检查网络，或者下载地址是否正确: " + url);
            }
        }
    }

    /// <summary>
    /// 请求图片
    /// </summary>
    /// <param name="url">图片地址,like 'http://www.shijing720.com/image.png '</param>
    /// <param name="action">请求发起后处理回调结果的委托,处理请求结果的图片</param>
    /// <returns></returns>
    IEnumerator _GetTexture(string url, Action<Texture2D> actionResult, Action errorAction)
    {
        yield return new WaitForSeconds(0.5f);
        UnityWebRequest uwr = new UnityWebRequest(url);
        DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
        uwr.downloadHandler = downloadTexture;
        yield return uwr.SendWebRequest();
        Texture2D t = null;
        if (!(uwr.isNetworkError || uwr.isHttpError))
        {
            t = downloadTexture.texture;
        }
        else
        {
            if (errorAction != null)
                errorAction.Invoke();
            Debug.LogError("下载失败，请检查网络，或者下载地址是否正确: " + url);
        }

        if (actionResult != null)
        {
            actionResult(t);
        }
    }

    /// <summary>
    /// 请求服务器地址上的音效
    /// </summary>
    /// <param name="url">没有音效地址,like 'http://www.shijing720.com/mysound.wav'</param>
    /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求结果的AudioClip</param>
    /// <param name="audioType">音效类型</param>
    /// <returns></returns>
    IEnumerator _GetAudioClip(string url, Action<AudioClip> actionResult, Action errorAction, AudioType audioType = AudioType.MPEG)
    {
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return uwr.SendWebRequest();
            if (!(uwr.isNetworkError || uwr.isHttpError))
            {
                if (actionResult != null)
                {
                    actionResult(DownloadHandlerAudioClip.GetContent(uwr));
                }
            }
            else
            {
                if (errorAction != null)
                    errorAction.Invoke();
                Debug.LogError("下载失败，请检查网络，或者下载地址是否正确: " + url);
            }
        }
    }


    /// <summary>
    /// 向服务器提交post请求
    /// </summary>
    /// <param name="serverURL">服务器请求目标地址,like "http://www.shijing720.com/myform"</param>
    /// <param name="lstformData">form表单参数</param>
    /// <param name="lstformData">处理返回结果的委托</param>
    /// <returns></returns>
    IEnumerator _Post(string serverURL, List<IMultipartFormSection> lstformData, Action<UnityWebRequest> actionResult)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
        UnityWebRequest uwr = UnityWebRequest.Post(serverURL, lstformData);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError("Post失败");
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            if (actionResult != null)
            {
                actionResult(uwr);
            }
        }
    }

    IEnumerator _Post(string serverURL, WWWForm form, Action<UnityWebRequest> actionResult)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
        UnityWebRequest uwr = UnityWebRequest.Post(serverURL, form);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError("Post失败");
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            if (actionResult != null)
            {
                actionResult(uwr);
            }
        }
    }

    /// <summary>
    /// 通过PUT方式将字节流传到服务器
    /// </summary>
    /// <param name="url">服务器目标地址 like 'http://www.shijing720.com/upload' </param>
    /// <param name="contentBytes">需要上传的字节流</param>
    /// <param name="resultAction">处理返回结果的委托</param>
    /// <param name="resultAction">设置header文件中的Content-Type属性</param>
    /// <returns></returns>
    IEnumerator _UploadByPut(string url, byte[] contentBytes, Action<bool> actionResult, string contentType = "application/octet-stream")
    {
        UnityWebRequest uwr = new UnityWebRequest();
        UploadHandler uploader = new UploadHandlerRaw(contentBytes);

        // Sends header: "Content-Type: custom/content-type";
        uploader.contentType = contentType;

        uwr.uploadHandler = uploader;

        yield return uwr.SendWebRequest();

        bool res = true;
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            res = false;
        }
        if (actionResult != null)
        {
            actionResult(res);
        }
    }
    #endregion
}