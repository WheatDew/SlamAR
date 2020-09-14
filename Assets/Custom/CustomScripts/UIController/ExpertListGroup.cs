using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertListGroup : MonoBehaviour
{

    private void Update()
    {
        if (API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back) || Input.GetKeyDown(KeyCode.B))
        {
            UIController uIController = FindObjectOfType<UIController>();
            if (uIController.GetCurrentMainGroup() == null)
            {
                uIController.CreateLoginGroup();
                uIController.DestroyExpertListGroup();
            }
        }
    }

    public void ExpertCall()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateExpertCallGroup();
        uIController.DestroyExpertListGroup();
    }


}
