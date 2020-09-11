using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAssetsVuforiaChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<GameObject,int>(EventID.SetAssetsVuforiaChild, SetChild);
    }


    private void OnDestroy()
    {
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<GameObject,int>(EventID.SetAssetsVuforiaChild, SetChild);
    }
    void SetChild(GameObject obj,int id)
    {
        if (id == GetComponent<MyTrackableEventHandler>().id)
            obj.transform.SetParent(transform);
    }
}
