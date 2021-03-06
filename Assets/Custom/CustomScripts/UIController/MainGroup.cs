﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGroup : MonoBehaviour
{
    public TextMesh exLog;

    public void DestroyAllChildren()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.DestroyCameraGroup();
        uIController.DestroyVideoGroup();
        uIController.DestroyExpertCallGroup();
        uIController.DestroyExpertListGroup();
    }

    public void TurnOnCamera()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.DestroyVideoGroup();
        uIController.DestroyExpertListGroup();
        uIController.DestroyExpertCallGroup();
        uIController.CreateCameraGroup(exLog);
    }

    public void TurnOnVideo()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.DestroyVideoGroup();
        uIController.DestroyExpertListGroup();
        uIController.DestroyExpertCallGroup();
        uIController.CreateVideoGroup(exLog);
    }

    public void RemoteAssistance()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateExpertListGroup();
        uIController.DestroyCameraGroup();
        uIController.DestroyVideoGroup();
    }

    private void Update()
    {
        if(API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back) || Input.GetKeyDown(KeyCode.B))
        {
            DestroyAllChildren();
        }
    }
}
