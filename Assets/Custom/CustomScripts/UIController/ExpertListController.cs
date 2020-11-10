using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertListController : MonoBehaviour
{
    public List<Expert> experts = new List<Expert>();
}



public struct Expert
{
    public string key;
    public string name;
    public string status;
}
