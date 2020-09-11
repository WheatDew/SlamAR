using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public GameObject mark;
    public Material selectedMaterial, unselectedMaterial;
    public int MaxMarks = 8;
    public float interval = 0.01f;
    List<GameObject> marks = new List<GameObject>();

    //void Start() {
    //    RefreshMarks(10,6);
    //}

    public void RefreshMarks(int amountMarks,int selectMark) {

        amountMarks = amountMarks > MaxMarks ? MaxMarks : amountMarks;
        
        if(amountMarks != marks.Count) {
            for(int i = 1; i < marks.Count; i++) {
                Destroy(marks[i]);
            }
            marks.Clear();
            marks.Add(mark);
            for(int i = 1; i < amountMarks; i++) {
                marks.Add(Instantiate(mark, transform));
            }
        }
        
        for(int i = 0; i < amountMarks; i++) {
            marks[i].gameObject.SetActive(true);
            if(selectMark-1 == i) {
                marks[i].GetComponentInChildren<MeshRenderer>().material = selectedMaterial;
            } else {
                marks[i].GetComponentInChildren<MeshRenderer>().material = unselectedMaterial;
            }

            if(i == 0) {
                marks[0].transform.localPosition = new Vector3(-( amountMarks - 1 ) * interval / 2, 0, -0.008f);
            } else {
                marks[i].transform.localPosition = new Vector3(marks[i - 1].transform.localPosition.x + interval, 0, -0.008f);
            }
            if(amountMarks == 1) {
                marks[0].gameObject.SetActive(false);
            }
        }
    }
}
