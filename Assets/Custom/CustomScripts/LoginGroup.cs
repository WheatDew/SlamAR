using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGroup : MonoBehaviour
{
    public GameObject Step1, Step2;
    public TextMesh TestLog;
    public TextMesh inputLinkString;

    private void Start()
    {
        Input.backButtonLeavesApp = false;
    }

    public void Update()
    {
        if (Step2.activeSelf && API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back))
        {
            //ResetStep();
        }
    }

    public void SetStart()
    {
        transform.localScale = Vector3.one;
        Step1.SetActive(true);
        Step2.SetActive(false);
    }

    public void SetQRCodeTaskGroup()
    {
        FindObjectOfType<QRCodeTaskGroup>().SetStart();
        transform.localScale = Vector3.zero;
    }

    public void SetRemoteAssistance()
    {
        FindObjectOfType<TaskSystemGroup>().SetRemoteAssistance();
        transform.localScale = Vector3.zero;
    }

    public void SetStep2()
    {
        Step1.SetActive(false);
        Step2.SetActive(true);
        //FindObjectOfType<SocketIOModule>().linkString = inputLinkString.text;
        TestLog.text = "";
    }

    public void ResetStep()
    {
        Step1.SetActive(true);
        Step2.SetActive(false);
    }
}
