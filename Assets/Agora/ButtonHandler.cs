using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    public GameObject go;
    /// <summary>
    ///   React to a button click event.  Used in the UI Button action definition.
    /// </summary>
    /// <param name="button"></param>
    public void onButtonClicked(Button button) 
    {
        if (go != null)
        {
            TestHome gameController = go.GetComponent<TestHome>();
            if (gameController == null)
            {
                Debug.LogError("Missing game controller...");
                return;
            }
            if (button.name == "JoinButton")
            {
                gameController.onJoinButtonClicked();
            }
            else if (button.name == "LeaveButton")
            {
                gameController.onLeaveButtonClicked();
            }
        }
    }
}
