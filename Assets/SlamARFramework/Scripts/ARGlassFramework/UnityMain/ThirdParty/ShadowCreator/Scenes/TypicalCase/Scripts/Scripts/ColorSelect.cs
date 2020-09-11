using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorSelect : MonoBehaviour {
	
	[SerializeField]
	private Color color;
	[SerializeField]
	private Vector3 startPos;
	[SerializeField]
	private Vector3 showPos;
	[HideInInspector]
	public bool isShow = false;


	// Use this for initialization
	void Start () {
//		Material mat = new Material(Shader.Find("Shader/line"));
//		mat.color = color;//设置颜色 
//		GetComponent<MeshRenderer>().material = mat;
	//	transform.localPosition = showPos;
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

	public void chooseColor()
	{
		PaintTool.canUse = false;
		PaintTool.mat = this.GetComponent<MeshRenderer> ().material;
    }

    void OpenUse()
    {
        PaintTool.canUse = true;
    }
}
