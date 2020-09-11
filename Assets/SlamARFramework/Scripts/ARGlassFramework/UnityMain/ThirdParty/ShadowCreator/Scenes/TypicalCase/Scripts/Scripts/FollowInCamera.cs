using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowInCamera : MonoBehaviour {
	[SerializeField]
	private float followTime = 2.0f;
	[SerializeField]
	private float MaxfollowDis = 4.0f;
	[SerializeField]
	private float MinfollowDis = 0.5f;
	[SerializeField]
	private float followAngle = 45.0f;

	private bool resetPos = false;
	private bool resetRot = false;
	private bool isMoving;

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Transform self;

	private float timedelay;
	private float alltimedelay;


	void Start()
	{
		
	}

	void Update()
	{  
		timedelay = timedelay + Time.deltaTime;
		//transform.LookAt (target);timedelay = timedelay + Time.deltaTime;
		if (timedelay > followTime/2) {
			timedelay = 0;
			if (!isMoving) {
				resetPos = checkDis ();
				resetRot = checkAngle ();
			} else {
				transform.DOMove (target.position, 0.5f);
				transform.DORotate (new Vector3 (0, target.transform.eulerAngles.y, 0), 0.5f);
				Debug.Log ("turn");
				resetPos = false;
				resetRot = false;
				isMoving = false;
				//				}
			}
			if (resetPos || resetRot) {
				isMoving = true;
			} 
		}
	}


	bool checkAngle()
	{
		float tarAngle = target.transform.eulerAngles.y > 180 ? target.transform.eulerAngles.y-360 : target.transform.eulerAngles.y;
		float selfAngle = transform.eulerAngles.y > 180 ?  transform.eulerAngles.y-360 : transform.eulerAngles.y;
		float angle = selfAngle -tarAngle;
		if (angle >followAngle || angle < -followAngle) {
			Debug.Log ("selfAngle:"+selfAngle);
			Debug.Log ("tarAngle:"+tarAngle);
			return true;
		}
		return false;
	}

	bool checkDis()
	{
		float dis = Vector3.Distance (new Vector3(target.position.x,0,target.position.z),new Vector3(self.transform.position.x,0,self.transform.position.z));
		if (dis > MaxfollowDis||dis < MinfollowDis) {
			return true;
		}
		return false;
	}

}
