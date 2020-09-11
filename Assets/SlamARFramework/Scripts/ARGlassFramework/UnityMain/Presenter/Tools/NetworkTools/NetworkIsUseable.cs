using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkIsUseable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //网络不可用            
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("无网络访问");
        }
        //无线网络
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("无线或网线");
        }
        //移动网络        
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("移动网络");
        }
    }

   
}
