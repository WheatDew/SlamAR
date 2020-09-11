using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientEventDispatcher
{
    private static ClientEventDispatcher instance;
    private ClienEventManager eventUIManager = new ClienEventManager();
    public static ClientEventDispatcher GetInstance()
    {
        if (instance == null) instance = new ClientEventDispatcher();
        return instance;
    }
    public ClienEventManager Manager
    {
        get
        {
            return eventUIManager;
        }
        private set { }
    }
}
