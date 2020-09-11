using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTool : MonoBehaviour {


	private GameObject clone;  
	private LineRenderer line;  
	private int i;
	private int index = 0;

	public static Material mat;
	public static bool canUse;
    public static float cuxi =1f;

	private Vector3 startPos; 
	private Vector3 endPos;

	public GameObject obs;

	// Use this for initialization
	void Start () {
		canUse = true;
	}
    private void OnEnable()
    {
        Invoke("OpenUse",0.1f);
    }

    void OpenUse()
    {
        canUse = true;
    }

    // Update is called once per frame  
    void Update () { 
		if (mat == null) {
			mat = new Material (Shader.Find ("Shader/line"));
			mat.color = Color.red;
		}
		this.GetComponent<MeshRenderer> ().material = mat;

		if (Input.GetMouseButtonDown (0)) {  
			if (canUse) {
				clone = (GameObject)Instantiate (obs, obs.transform.position, obs.transform.rotation);//克隆一个带有LineRender的物体   
				clone.name+=index.ToString();
				line = clone.AddComponent<LineRenderer> ();//获得该物体上的LineRender组件  
				line.material = new Material(Shader.Find("Shader/Additive"));
				line.SetColors (mat.color,mat.color);
				line.SetWidth (0.02f* cuxi, 0.02f* cuxi);//设置宽度  
				i = 0; 
				Debug.Log("Color.red:"+Color.clear);
				ToolManager.toolResult.Add (clone);
				startPos = this.transform.position;
				index++;
			} 
			else {
				canUse = true;
				line = null;
			}
        }
        if (canUse)
        {
            if (Input.GetMouseButton(0))
            {
                transform.parent.GetComponent<ToolManager>().followFocus = false;
                if (line != null)
                {
                    i++;
                    line.SetVertexCount(i);//设置顶点数  
                    line.SetPosition(i - 1, this.transform.position);//设置顶点位置 
                    ToolManager.pointsOfLine[this.transform.position] = clone.name;
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            transform.parent.GetComponent<ToolManager>().followFocus = true;
            endPos = this.transform.position;
            //			if (line != null) {
            //				addColliderToLine ();
            //			}
            line = null;
        }
    }  
	private void addColliderToLine()
	{
		BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
		col.transform.parent = line.transform; // Collider is added as child object of line
		float lineLength = Vector3.Distance(startPos, endPos); // length of line
		col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
		Vector3 midPoint = (startPos + endPos) / 2;
		col.transform.position = midPoint; // setting position of collider object
		// Following lines calculate the angle between startPos and endPos
		float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
		if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
		{
			angle *= -1;
		}
		angle = Mathf.Rad2Deg * Mathf.Atan(angle);
		col.transform.Rotate(0, 0, angle);
	}
}