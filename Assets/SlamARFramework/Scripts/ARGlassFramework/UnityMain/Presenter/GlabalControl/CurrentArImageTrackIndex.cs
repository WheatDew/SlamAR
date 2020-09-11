using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentArImageTrackIndex : MonoSingleton<CurrentArImageTrackIndex>
{
    //[HideInInspector]
    public int currentIndex = -1;
    //[HideInInspector]
    public Vector3 trackRotate;
    //[HideInInspector]
    public Vector3 trackPosition;
}
