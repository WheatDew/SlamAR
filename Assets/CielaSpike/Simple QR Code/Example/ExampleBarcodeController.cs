using UnityEngine;
using System.Collections;

using CielaSpike.Unity.Barcode;
using System.Threading;

public class ExampleBarcodeController : MonoBehaviour
{
    WebCamTexture cameraTexture;

    Material cameraMat;
    GameObject plane;

    WebCamDecoder decoder;

    IBarcodeEncoder qrEncoder, pdf417Encoder;

    Vector2 scroll = Vector2.zero;

    public GameObject displayPlane;

    IEnumerator Start1()
    {
        // get render target;
        plane = displayPlane;
        cameraMat = plane.GetComponent<MeshRenderer>().material;

        // get a reference to web cam decoder component;
        decoder = GetComponent<WebCamDecoder>();

        // get encoders;
        qrEncoder = Barcode.GetEncoder(BarcodeType.QrCode, new QrCodeEncodeOptions()
        {
            ECLevel = QrCodeErrorCorrectionLevel.H
        });

        pdf417Encoder = Barcode.GetEncoder(BarcodeType.Pdf417);

        qrEncoder.Options.Margin = 1;
        pdf417Encoder.Options.Margin = 2;

        var devices = WebCamTexture.devices;
        var deviceName = devices[0].name;
        cameraTexture = new WebCamTexture(deviceName, 800, 600);
        cameraTexture.Play();

        // start decoding;
        yield return StartCoroutine(decoder.StartDecoding(cameraTexture));

        cameraMat.mainTexture = cameraTexture;

        // adjust texture orientation;
        plane.transform.rotation = plane.transform.rotation *
            Quaternion.AngleAxis(cameraTexture.videoRotationAngle, Vector3.up);
    }

    public void Update()
    {
        if (decoder.Result!=null&&decoder.Result.Success)
            Debug.Log(decoder.Result.Text);
    }

    public void ReStart()
    {
        StartCoroutine(Start1());
    }

    private void OnDisable()
    {
        if (cameraTexture != null)
        {
            cameraTexture.Stop();
            Destroy(cameraTexture);
        }
    }
}
