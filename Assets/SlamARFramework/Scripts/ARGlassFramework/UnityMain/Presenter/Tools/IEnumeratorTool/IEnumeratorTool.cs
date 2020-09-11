using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumeratorTool : MonoBehaviourInstance<IEnumeratorTool>
{
	WaitForSeconds m_waitForOneSecond = new WaitForSeconds(1.0f);
	public WaitForSeconds waitForOneSecond
	{
		get { return m_waitForOneSecond; }
	}
}
