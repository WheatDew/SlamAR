using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomManager : MonoBehaviour {


	public GameObject tools;
	public GameObject sign_di;
	public GameObject color_di;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickColor()
	{
		PaintTool.canUse = false;
		Transform btn_color = transform.Find("btn_color");
		Transform color_buttoms = btn_color.Find("color_buttoms");
		for (int i = 0; i < color_buttoms.transform.childCount; i++) {  
			GameObject child = color_buttoms.transform.GetChild (i).gameObject;
			if (child.GetComponent<ColorSelect> ().isShow) {
				child.GetComponent<ColorSelect> ().close ();
				child.SetActive (false);
				color_di.SetActive (false);
			} else {
				child.GetComponent<ColorSelect> ().show ();
				child.SetActive (true);
				color_di.SetActive (true);
			}
		}
	}

	public void clickClean()
	{
		PaintTool.canUse = false;
		tools.GetComponent<ToolManager> ().useClean ();
	}

	public void clickSignItem()
	{
		PaintTool.canUse = false;
		Transform btn_sign = transform.Find("btn_sign");
		Transform sign_items = btn_sign.Find("sign_items");
		for (int i = 0; i < sign_items.transform.childCount; i++) {  
			GameObject child = sign_items.transform.GetChild (i).gameObject;
			if (child.GetComponent<SignSelect> ().isShow) {
				child.GetComponent<SignSelect> ().close ();
				child.SetActive (false);
				sign_di.SetActive (false);
			} else {
				child.GetComponent<SignSelect> ().show ();
				sign_di.SetActive (true);
				child.SetActive (true);
			}
		}
	}

	public void clickPaint()
	{
	//	PaintTool.canUse = false;
        tools.GetComponent<ToolManager>().usePaint();
	}

    public void setCuXi(float a)
    {
        PaintTool.cuxi = a;
    }
}
