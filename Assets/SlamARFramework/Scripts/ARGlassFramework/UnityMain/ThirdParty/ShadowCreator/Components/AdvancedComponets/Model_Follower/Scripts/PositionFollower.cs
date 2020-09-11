using DG.Tweening;
using SC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour {
    

    Vector3 worldPoint = Vector3.zero;
    [SerializeField]
    private float delDistance = 1;
    [SerializeField]
    private float followTime = 2;
    //[SerializeField]
    //private float followAngle = 60;

    private Vector3 lastestPosition;

    Coroutine followCoroutine;

    // Use this for initialization
    void OnEnable() {
        followCoroutine = StartCoroutine(Following(followTime));
        lastestPosition = transform.position;
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
                worldPoint = SvrManager.Instance.leftCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                if (Vector3.Distance(lastestPosition, SvrManager.Instance.leftCamera.transform.position) > delDistance) {
                    Debug.Log("Start PositionFollower");
                    lastestPosition = transform.position;
                    //Vector3 angle = SvrManager.Instance.leftCamera.transform.eulerAngles;

                    //transform.DORotate(new Vector3(0, angle.y, 0), 0.5f).SetEase(Ease.InQuart).SetAutoKill(true);
                    //transform.DORotate(angle, 0.2f).SetEase(Ease.InQuart).SetAutoKill(true);
                    transform.DOMove(worldPoint, 0.5f).SetEase(Ease.OutCubic).SetAutoKill(true);
                }
            }

        }
    }
}
