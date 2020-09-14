using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertCallGroup : MonoBehaviour
{

    

    public void CancelCalling()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateExpertListGroup();
        uIController.DestroyExpertCallGroup();
    }
}
