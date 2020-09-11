using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCodeGroup : MonoBehaviour
{
    public WebCamDecoder webCamDecoder;
    public ExampleBarcodeController exampleBarcodeController;

    private void Start()
    {
        exampleBarcodeController.ReStart();
    }

    private void Update()
    {
        if ((webCamDecoder.Result != null && webCamDecoder.Result.Text != null && webCamDecoder.Result.Text.Equals("老人正确跌倒ar项目"))||Input.GetKeyDown(KeyCode.L))
        {
            UIController uIController = FindObjectOfType<UIController>();
            uIController.CreateTaskGroup();
            uIController.DestroyQRCodeGroup();
        }
    }
}
