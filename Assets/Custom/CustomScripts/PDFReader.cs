using Paroxe.PdfRenderer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PDFReader : MonoBehaviour
{
    public int m_Page = 0;
    public GameObject link;
    string downloadFileName = "";

    private void Start()
    {
        link.SetActive(false);
    }

    public void StartPrintPDF(string url,string fileName)
    {
        Debug.Log(url + fileName);
        StopAllCoroutines();
        StartCoroutine(PrintPDF(url, fileName));
    }

    public void StartPrintImage(string url)
    {
        StopAllCoroutines();
        StartCoroutine(PrintImage(url));
    }

    public IEnumerator PrintImage(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        yield return wr.SendWebRequest();
        if (!wr.isNetworkError)
        {
            Texture2D tex = null;
            tex = texDl.texture;
            link.SetActive(true);
            link.GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

    public IEnumerator PrintPDF(string url,string contentName)
    {
        
#if UNITY_EDITOR
        downloadFileName = Path.Combine(Application.dataPath, contentName);
#elif UNITY_ANDROID
        downloadFileName = Path.Combine(Application.persistentDataPath, contentName);
#endif
        Debug.Log("2:"+url + contentName);

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

        PDFDocument pdfDocument = new PDFDocument(downloadFileName);


        if (pdfDocument.IsValid)
        {
            int pageCount = pdfDocument.GetPageCount();

            PDFRenderer renderer = new PDFRenderer();
            Texture2D tex = renderer.RenderPageToTexture(pdfDocument.GetPage(m_Page % pageCount), 1024, 1024);

            tex.filterMode = FilterMode.Bilinear;
            tex.anisoLevel = 8;
            link.SetActive(true);
            link.GetComponent<MeshRenderer>().material.mainTexture = tex;
        }

    }



    public void NextPageClick()
    {
        m_Page++;
        PrintLocalPDF();
    }

    public void LastPageClick()
    {
        m_Page--;
        PrintLocalPDF();
    }

    public void PrintLocalPDF()
    {

        PDFDocument pdfDocument = new PDFDocument(downloadFileName);


        if (pdfDocument.IsValid)
        {
            int pageCount = pdfDocument.GetPageCount();

            PDFRenderer renderer = new PDFRenderer();
            Texture2D tex = renderer.RenderPageToTexture(pdfDocument.GetPage(m_Page % pageCount), 1024, 1024);

            tex.filterMode = FilterMode.Bilinear;
            tex.anisoLevel = 8;
            link.SetActive(true);
            link.GetComponent<MeshRenderer>().material.mainTexture = tex;
        }
    }
}
