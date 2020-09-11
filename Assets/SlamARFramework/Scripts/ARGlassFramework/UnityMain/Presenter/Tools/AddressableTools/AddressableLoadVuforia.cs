using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableLoadVuforia : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        AddressableTools.InstantiateAsync("Vuforia", null);
    }


}
