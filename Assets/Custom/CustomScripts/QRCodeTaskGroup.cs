using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCodeTaskGroup : MonoBehaviour
{

    public GameObject Step1, Step2;
    public ExampleBarcodeController exampleBarcodeController;
    public WebCamDecoder webCamDecoder;
    public Transform plane;
    private Vector3 planePosition;
    public Texture2D texture2D;

    private void Start()
    {
        planePosition = plane.position;
        plane.gameObject.SetActive(false);
    }

    public void SetStart()
    {
        exampleBarcodeController.gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        plane.gameObject.SetActive(true);
        exampleBarcodeController.ReStart();
    }



    public void SetSetp1()
    {

    }

    public void SetStep2()
    {
        plane.position = planePosition;


    }

    public void SetTaskSystemGroup()
    {

        exampleBarcodeController.StopAllCoroutines();
        webCamDecoder.StopDecoding();
        FindObjectOfType<TaskSystemGroup>().SetStart();
        transform.localScale = Vector3.zero;
    }


    private void Update()
    {
        if (webCamDecoder.Result!=null&& webCamDecoder.Result.Text!=null&& webCamDecoder.Result.Text.Equals("老人正确跌倒ar项目"))
        {
            webCamDecoder.ResetResult(texture2D);
            exampleBarcodeController.StopAllCoroutines();
            webCamDecoder.StopDecoding();
            SetStep2();

        }
        if (API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back)||Input.GetMouseButtonDown(2))
        {
            //if (Children[1].activeSelf)
            //{
            //    exampleBarcodeController.StopAllCoroutines();

            //    webCamDecoder.StopDecoding();
            //    foreach (var item in Children)
            //    {
            //        item.SetActive(false);
            //    }
            //    transform.localScale = Vector3.zero;
            //    exampleBarcodeController.gameObject.SetActive(false);
            //    FindObjectOfType<LoginGroup>().SetStart();
            //}
            //else if (Children[0].activeSelf)
            //{
            //    exampleBarcodeController.StopAllCoroutines();

            //    webCamDecoder.StopDecoding();
            //    foreach (var item in Children)
            //    {
            //        item.SetActive(false);
            //    }
            //    transform.localScale = Vector3.zero;
            //    exampleBarcodeController.gameObject.SetActive(false);
            //    FindObjectOfType<LoginGroup>().SetStart();
            //}
        }
    }
}
