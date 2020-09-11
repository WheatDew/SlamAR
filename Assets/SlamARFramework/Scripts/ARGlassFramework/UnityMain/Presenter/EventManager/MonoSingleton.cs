using UnityEngine;
using System.Collections;
using System.Threading;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    //private static Mutex mutex;
    public static T GetInstance
    {
        get
        {
            if (m_Instance == null)
            {
                //m_Instance = new GameObject( typeof(T).Name).GetComponent<T>();
            }
            return m_Instance;
        }
    }

    public void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
    }
}