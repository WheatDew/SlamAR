using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTool_old : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame  
	void Update () { 
		
	}  

	public void cleanLast()
	{
		if (ToolManager.toolResult.Count>0) {
			Destroy(ToolManager.toolResult[ToolManager.toolResult.Count-1]);
			ToolManager.toolResult.RemoveAt (ToolManager.toolResult.Count-1);
		}
	}
}