using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFPS : MonoBehaviour
{
    Game3DInputField inputField;
    void Start() {
        inputField = GetComponentInChildren<Game3DInputField>();
        API_SVR.SetRenderFrame(75);
    }

    public void SetFPS() {

        if(inputField) {
            API_SVR.SetRenderFrame(int.Parse(inputField.text));
        }

    }
}
