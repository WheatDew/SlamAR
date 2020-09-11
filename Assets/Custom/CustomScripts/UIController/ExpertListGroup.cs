using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertListGroup : MonoBehaviour
{
    public void ExpertCall()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateExpertCallGroup();
        uIController.DestroyExpertListGroup();
    }


}
