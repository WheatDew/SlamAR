using Paroxe.PdfRenderer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DocumentGroup : MonoBehaviour
{
    public int m_Page = 0;
    public GameObject link;
    string downloadFileName = "";

    private void Start()
    {
        PrintLocalPDF();
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

#if UNITY_EDITOR
        downloadFileName = Path.Combine(Application.dataPath, "ckk.pdf");
#elif UNITY_ANDROID
        downloadFileName = Path.Combine(Application.persistentDataPath, contentName);
#endif

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
