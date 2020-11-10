using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class FileDownload : MonoBehaviour
{
    public string downloadFileName;
    public string contentName;
    public string url;

    public IEnumerator DownloadPDF()
    {
#if UNITY_EDITOR
        downloadFileName = Path.Combine(Application.dataPath, contentName);
#elif UNITY_ANDROID
        downloadFileName = Path.Combine(Application.persistentDataPath, contentName);
#endif
        Debug.Log("2:" + url + contentName);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url + contentName))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                DownloadHandler fileHandler = webRequest.downloadHandler;
                using (MemoryStream memory = new MemoryStream(fileHandler.data))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    FileStream file = File.Open(downloadFileName, FileMode.OpenOrCreate);
                    int readBytes;
                    while ((readBytes = memory.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        file.Write(buffer, 0, readBytes);
                    }
                    file.Close();
                }
            }
        }
    }
}
