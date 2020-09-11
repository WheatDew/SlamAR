using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;
using SC;
using SC.InputSystem.InputDeviceHead;

public class CleanTool : MonoBehaviour {
	// Use this for initialization
	GameObject Gazer;
    Focus focs;
	void Start () {
        /*
        focs = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.lineIndicate.endOfPointWhenTarget.focus;*/

    }

	// Update is called once per frame  
	void Update () { 
        /*
		string destortItem = "";
		if (!Gazer||Gazer != InputSystem.Gazer) {
			Gazer = InputSystem.Gazer;
		}
		foreach (KeyValuePair<Vector3,string> dir in ToolManager.pointsOfLine) {
			///Debug.Log ("x:" + dir.Key.x + ",y:" + dir.Key.y + ",z:" + dir.Key.z);
			if (DisPoint2Line (dir.Key,InputSystem.Gazer.transform.position,transform.parent.position)<=0.02f)
			{
				destortItem = dir.Value;
				transform.parent.position = dir.Key;
				break;
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			if (PaintTool.canUse) {
				if (focs.endOfPointWhenTarget != null) {
					string[] names = focs.endOfPointWhenTarget.name.Split ('_');
                    Debug.Log("names=========>"+names);
					if (names [names.Length - 1] == "sign" && ToolManager.toolResult.Contains (focs.endOfPointWhenTarget.gameObject)) {
						Destroy (focs.endOfPointWhenTarget);
						ToolManager.toolResult.Remove (focs.endOfPointWhenTarget.gameObject);
						return;
					}
				}
			} else {
				PaintTool.canUse = true;
			}
			List<Vector3> nameList = new List<Vector3>();
			if (destortItem != "") {
				Destroy (GameObject.Find (destortItem));
				foreach (KeyValuePair<Vector3,string> dir in ToolManager.pointsOfLine) {
					if (dir.Value==destortItem){
						nameList.Add (dir.Key);
					}
				}
				for(int i =0;i<nameList.Count;i++)
				{
					ToolManager.pointsOfLine.Remove (nameList [i]);
				}
				Debug.Log (destortItem);
			}
		}*/
	}  

	public void cleanLast()
	{
		if (ToolManager.toolResult.Count>0) {
			Destroy(ToolManager.toolResult[ToolManager.toolResult.Count-1]);
			ToolManager.toolResult.RemoveAt (ToolManager.toolResult.Count-1);
		}
	}

	float DisPoint2Line(Vector3 point,Vector3 linePoint1,Vector3 linePoint2)
	{
		Vector3 vec1 = point - linePoint1;
		Vector3 vec2 = linePoint2 - linePoint1;
		Vector3 vecProj = Vector3.Project(vec1, vec2);
		float dis =  Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));
		return dis;
	}

}