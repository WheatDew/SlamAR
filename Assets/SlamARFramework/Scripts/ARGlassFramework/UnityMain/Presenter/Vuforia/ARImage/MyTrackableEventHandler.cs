using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public delegate void VuforiaFind(GameObject gameObject);
public delegate void VuforiaLost(GameObject gameObject);

/// <summary>
/// Vuforia识别图相关事件
/// </summary>
public class MyTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

    public event VuforiaFind onVuforiaFind;
    public event VuforiaLost onVuforiaLost;

    public int id;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        DownloadAllPrepositionAssets.GetInstance.hasDownVuforia = true;
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " " + mTrackableBehaviour.CurrentStatus + " -- " + mTrackableBehaviour.CurrentStatusInfo);
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            OnTrackingLost();
        }
    }
    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (CurrentArImageTrackIndex.GetInstance.currentIndex == id) return;
        Debug.Log(111);
        transform.localScale = Vector3.one;
        CurrentArImageTrackIndex.GetInstance.trackRotate = ControlTransform.GetInstance.transform.eulerAngles/* + transform.eulerAngles + new Vector3(90, 0, 0)*/;
        CurrentArImageTrackIndex.GetInstance.trackPosition = ControlTransform.GetInstance.transform.position;
        CurrentArImageTrackIndex.GetInstance.currentIndex = id;
        ClientEventDispatcher.GetInstance().Manager.TriggerEvent<Vector3>(EventID.SetAssetsParentPosition, CurrentArImageTrackIndex.GetInstance.trackPosition);
        ClientEventDispatcher.GetInstance().Manager.TriggerEvent<Vector3>(EventID.SetAssetsParentRotate, CurrentArImageTrackIndex.GetInstance.trackRotate);
        print("Y轴角度偏移" + Mathf.Acos(Vector3.Dot(new Vector3(0, 0, 1), transform.up)) * Mathf.Rad2Deg);
        ARCameraInit.GetInstance.gameObject.SetActive(false);
        TrackableScanCount.GetInstance.WhiteJson(mTrackableBehaviour.TrackableName, DateTime.Now.ToString());
        if (onVuforiaFind != null)
            onVuforiaFind.Invoke(gameObject);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        ClientEventDispatcher.GetInstance().Manager.TriggerEvent<bool>(EventID.UICanvasWithHeadCamera, false);
    }


    protected virtual void OnTrackingLost()
    {
        if (SaveHotfixAssets.GetInstance && SaveHotfixAssets.GetInstance.hotfixTargetsParties.Length > 0)
        {
            var temp = SaveHotfixAssets.GetInstance.hotfixTargetsParties[id].transform.GetComponentsInChildren<IHotfix>();
            if (temp.Length > 0)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i].OnVuforiaLost();
                }
            }
        }
        if (onVuforiaLost != null)
            onVuforiaLost.Invoke(gameObject);
    }

    #endregion // PROTECTED_METHODS
}
