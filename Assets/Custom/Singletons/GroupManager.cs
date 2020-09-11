using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager
{
    private static readonly GroupManager instance =new GroupManager();

    public Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();

    static GroupManager()
    {
    }
    private GroupManager()
    {
    }
    public static GroupManager Instance
    {
        get
        {
            return instance;
        }
    }
}
