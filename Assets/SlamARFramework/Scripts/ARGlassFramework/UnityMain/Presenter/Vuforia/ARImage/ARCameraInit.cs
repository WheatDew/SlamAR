using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARCameraInit : MonoSingleton<ARCameraInit>
{
    VuforiaBehaviour behaviour;
    void Start()
    {
        behaviour = GetComponent<VuforiaBehaviour>();
    }
}
