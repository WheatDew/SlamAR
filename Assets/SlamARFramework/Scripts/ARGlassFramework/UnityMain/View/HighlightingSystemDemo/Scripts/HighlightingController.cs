using UnityEngine;
using System.Collections;

public class HighlightingController : MonoBehaviour
{
	protected HighlightableObject ho;
	
	void Awake()
	{
		ho = gameObject.AddComponent<HighlightableObject>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab)) 
		{
			ho.ConstantSwitch();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			ho.ConstantSwitchImmediate();
		}
		if (Input.GetKeyDown(KeyCode.Z)) 
		{
			ho.Off();
		}
		
		AfterUpdate();
	}
	
	protected virtual void AfterUpdate() {}
}