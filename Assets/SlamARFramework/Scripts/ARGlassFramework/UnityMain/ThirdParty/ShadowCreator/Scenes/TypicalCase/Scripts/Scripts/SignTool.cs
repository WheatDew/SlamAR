using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC;

public class SignTool : MonoBehaviour {

	public static GameObject SignItemPrefab;
	int signCount = 0;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame  
	void Update () { 
		if (Input.GetMouseButtonDown (0)) {  
			if (PaintTool.canUse) {
				transform.parent.GetComponent<ToolManager> ().followFocus = false;
				GameObject sign = (GameObject)Instantiate (SignItemPrefab);
				sign.name = "sign_" + signCount.ToString ();
				sign.transform.position = this.transform.position;
				sign.transform.LookAt (SvrManager.Instance.head.transform);
				ToolManager.toolResult.Add (sign);
				signCount++;
			} else {
				PaintTool.canUse = true;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			transform.parent.GetComponent<ToolManager> ().followFocus = true;
		}
	}  
}