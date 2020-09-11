using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyManager : MonoBehaviour
{
    public bool isChoose;
    public Animator an;
    public GameObject an1;
    public GameObject an2;
    public void clearObj()
    {
        Invoke("clearThis",10f);
    }
    void clearThis()
    {
        Destroy(this.gameObject);
    }
}
