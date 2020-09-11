using SC.InputSystem;
using UnityEngine;

public class BackKeyDialog : PointerDelegate
{
    public BackKeyDialogUI UI;
    

    public void BackKey() { 
        
        if(!UI)
            return;
        if(UI.gameObject.activeSelf == true) {
            Application.Quit();
        } else {
            UI.gameObject.SetActive(true);
        }
    }

}
