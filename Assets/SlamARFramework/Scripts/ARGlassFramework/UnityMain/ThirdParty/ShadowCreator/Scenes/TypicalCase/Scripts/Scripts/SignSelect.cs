using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SignSelect : MonoBehaviour {

	[SerializeField]
	private Vector3 startPos;
	[SerializeField]
	private Vector3 showPos;
	[HideInInspector]
	public bool isShow = false;
	[SerializeField]
	private GameObject signItem;
	// Use this for initialization
	void Start () {
		transform.localPosition = showPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void show()
	{
//		Sequence appearSequence = DOTween.Sequence();
//		Tween tween = transform.DOLocalMove (showPos, 0.5f);
//		tween.SetEase (Ease.InOutCirc);
//		appearSequence.Append (tween);
		isShow = true;
	}

	public void close()
	{
//		Sequence appearSequence = DOTween.Sequence();
//		Tween tween = transform.DOLocalMove (startPos, 0.5f);
//		tween.SetEase (Ease.InOutCirc);
//		appearSequence.Append (tween);
		isShow = false;
	}

	public void clickItem()
	{
		PaintTool.canUse = false;
		SignTool.SignItemPrefab = signItem;
		this.transform.parent.parent.parent.GetComponent<ButtomManager> ().tools.GetComponent<ToolManager> ().useSign();
	}
}
