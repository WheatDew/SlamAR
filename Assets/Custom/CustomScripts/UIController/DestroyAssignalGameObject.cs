using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAssignalGameObject : MonoBehaviour
{
    public GameObject AssignalGameobject;

    void Start()
    {
        GetComponent<SCButton>().onClick.AddListener(delegate
        {
            Destroy(AssignalGameobject);
        });
    }

}
