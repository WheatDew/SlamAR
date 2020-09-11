using UnityEngine;

namespace Paroxe.PdfRenderer.Examples
{
    public class PDFDocumentRenderToTextureExample : MonoBehaviour
    {
        public int m_Page = 0;
        public GameObject link;

#if !UNITY_WEBGL

        void Start()
        {
            PDFDocument pdfDocument = new PDFDocument(PDFBytesSupplierExample.PDFSampleByteArray, "");

            //PDFDocument pdfDocument = new PDFDocument("",);


            if (pdfDocument.IsValid)
            {
                int pageCount = pdfDocument.GetPageCount();

                PDFRenderer renderer = new PDFRenderer();
                Texture2D tex = renderer.RenderPageToTexture(pdfDocument.GetPage(m_Page % pageCount), 1024, 1024);

                tex.filterMode = FilterMode.Bilinear;
                tex.anisoLevel = 8;

                link.GetComponent<MeshRenderer>().material.mainTexture = tex;
            }
        }
#endif
    }
}
