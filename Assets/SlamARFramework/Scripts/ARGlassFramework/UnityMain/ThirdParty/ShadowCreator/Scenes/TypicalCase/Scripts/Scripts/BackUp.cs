using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUp : MonoBehaviour
{
    private Vector3 initPos;
    private Vector3 initRot;
    public Transform plane;

    public FlyManager go;
    private GameObject goold;
    // Start is called before the first frame update
    void Start()
    {
        if(go!=null)
        {
            goold = GameObject.Instantiate(go.gameObject, this.transform);
            go.gameObject.SetActive(false);
        }

        initPos = this.transform.position;
        initRot = this.transform.eulerAngles;
    }

    public void DownClick()
    {
    }

    public void UpClick()
    {
        FlyManager flm = goold.GetComponent<FlyManager>();
        if(!flm.isChoose)
        {

            flm.isChoose = true;
            flm.an2.SetActive(true);
            flm.an1.SetActive(true);
            flm.an.enabled = true;
            flm.an.Play(0);
            flm.transform.parent = null;
            flm.clearObj();

            Invoke("initFly", 3f);
        }
    }

    void initFly()
    {
        goold = GameObject.Instantiate(go.gameObject, this.transform);
        goold.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(plane.position, this.transform.position);
        if(dis>100)
        {
            this.transform.position = initPos;
            this.transform.eulerAngles = initRot;
        }
    }
}
