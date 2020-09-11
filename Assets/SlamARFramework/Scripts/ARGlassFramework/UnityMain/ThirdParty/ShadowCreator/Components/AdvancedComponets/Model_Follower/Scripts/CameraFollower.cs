using DG.Tweening;
using SC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    
    Vector3 viewPoint = Vector3.zero;
    Vector3 worldPoint = Vector3.zero;
    [SerializeField]
    private float followDistance = 2;
    [SerializeField]
    private float followTime = 2;
    //[SerializeField]
    //private float followAngle = 60;

    Coroutine followCoroutine;

    // Use this for initialization
    void OnEnable() {
        followCoroutine = StartCoroutine(Following(followTime));
    }

    void Disable() {
        if (followCoroutine != null) {
            StopCoroutine(followCoroutine);
        }
    }

    IEnumerator Following(float time) {
        while (true) {
            yield return new WaitForSeconds(time);
            yield return null;
            if (SvrManager.Instance && SvrManager.Instance.IsRunning) {
                viewPoint = SvrManager.Instance.leftCamera.WorldToViewportPoint(transform.position);
                worldPoint = SvrManager.Instance.leftCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, followDistance));
                if (viewPoint.x < 0 || viewPoint.y < 0 || viewPoint.x >1 || viewPoint.y >1) {
                    Debug.Log("Start Following");
                    Vector3 angle = SvrManager.Instance.leftCamera.transform.eulerAngles;

                    //transform.DORotate(new Vector3(0, angle.y, 0), 0.5f).SetEase(Ease.InQuart).SetAutoKill(true);
                    transform.DORotate(angle, 0.2f).SetEase(Ease.InQuart).SetAutoKill(true);
                    transform.DOMove(worldPoint, 0.5f).SetEase(Ease.OutCubic).SetAutoKill(true);
                }
            }

        }
    }
}
