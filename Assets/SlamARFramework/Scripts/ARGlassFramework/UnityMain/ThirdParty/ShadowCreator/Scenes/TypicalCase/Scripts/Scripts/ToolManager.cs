using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SC.InputSystem;

public class ToolManager : MonoBehaviour {

	public GameObject focus;
	public GameObject tool_paint;
	public GameObject tool_clean;
	public GameObject tool_sign;
	[HideInInspector]
	public bool followFocus = true;

	public static List<GameObject> toolResult = new List<GameObject>();

	public static Dictionary<Vector3,string> pointsOfLine = new Dictionary<Vector3,string>();
    
    void Start()
    {
    }
    float times;
	// Update is called once per frame
	void Update () {
        /*
        times = times + Time.deltaTime;
        if(times>1)
        {
            if (InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.lineIndicate.endOfPointWhenTarget.gameObject.activeSelf)
            {
                focus = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.lineIndicate.endOfPointWhenTarget.gameObject;
            }
            else
            {
                focus = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.lineIndicate.endOfPointWhenNoTarget.gameObject;
            }
            transform.position = focus.transform.position;
            times = 2;
        }*/
    }

	public void useClean()
	{
		tool_paint.SetActive (false);
		tool_clean.SetActive (true);
		tool_sign.SetActive (false);
		//tool_clean.GetComponent<CleanTool> ().cleanLast ();
	}

	public void useSign()
	{
		tool_paint.SetActive (false);
		tool_clean.SetActive (false);
		tool_sign.SetActive (true);
	}

	public void usePaint()
	{
		tool_paint.SetActive (true);
		tool_clean.SetActive (false);
		tool_sign.SetActive (false);
	}
}
