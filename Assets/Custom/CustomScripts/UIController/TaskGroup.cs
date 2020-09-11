using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGroup : MonoBehaviour
{
    public void ImmediateExecute()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateMainGroup();
        uIController.DestroyTaskGroup();
    }
}
