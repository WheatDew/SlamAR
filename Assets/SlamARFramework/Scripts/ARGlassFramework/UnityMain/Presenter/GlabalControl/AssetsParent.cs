using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsParent : MonoBehaviour
{
    [SerializeField]
    Transform child;
    void Start()
    {
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<GameObject>(EventID.SetAssetsParent, SetParent);
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<GameObject>(EventID.SetAssetsChildParent, SetChildParent);
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<Vector3>(EventID.SetAssetsChildPosition, SetChildPosition);
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<Vector3>(EventID.SetAssetsChildRotate, SetChildRotate);
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<Vector3>(EventID.SetAssetsParentPosition, SetPosition);
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<Vector3>(EventID.SetAssetsParentRotate, SetRotate);
    }

    private void OnDestroy()
    {
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<GameObject>(EventID.SetAssetsParent, SetParent);
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<Vector3>(EventID.SetAssetsChildRotate, SetChildRotate);
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<Vector3>(EventID.SetAssetsChildPosition, SetChildPosition);
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<GameObject>(EventID.SetAssetsChildParent, SetChildParent);
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<Vector3>(EventID.SetAssetsParentPosition, SetPosition);
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<Vector3>(EventID.SetAssetsParentRotate, SetRotate);
    }

    void SetParent(GameObject obj)
    {
        obj.transform.SetParent(transform);
    }

    void SetChildParent(GameObject obj)
    {
        obj.transform.SetParent(child);
    }

    void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    void SetRotate(Vector3 rotate)
    {
        transform.eulerAngles = rotate;
    }

    void SetChildPosition(Vector3 p)
    {
        child.position = p;
    }

    void SetChildRotate(Vector3 p)
    {
        child.eulerAngles = p;
    }
}
