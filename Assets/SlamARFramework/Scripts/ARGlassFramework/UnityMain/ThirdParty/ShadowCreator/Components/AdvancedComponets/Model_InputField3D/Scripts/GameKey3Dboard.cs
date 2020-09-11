using System.Collections;
using System.Collections.Generic;
using UnityEngine;   

public class GameKey3Dboard : MonoBehaviour {
     
    public GameObject keyboard_num;
    public GameObject keyboard_symbol;
    public GameObject keyboard_enUp;
    public GameObject keyboard_enLow;    
 
    private Game3DInputField input;                                 
    private List<string> str;
     
    // Use this for initialization
    void Awake()
    {
        str = new List<string>();
       // Debug.Log("GameKey3Dboard Awake"  + this.name);
       // InitTween();
    }

    public void show(Game3DInputField input, string value)
    {
       // Debug.Log("GameKey3Dboard ++++ show");
        this.input = input;
        str = new List<string>();
        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                str.Add(value[i].ToString());
            }
        }
        gameObject.SetActive(true);
        showEnLow();
       // Begin();
    }

    public void onClick(string value)
    {
        if(str.Count >= input.maxLength)
        {
            return;
        }
        str.Add(value);
        setTextString();
    }

    public void clear()
    {
        str = new List<string>();
        setTextString();
    }

    public void done()
    {
        str = new List<string>();
        if(this!=null)
        gameObject.SetActive(false);
    }

    public void del()
    {
        if (str.Count > 0)
        {
            str.RemoveAt(str.Count - 1);
            setTextString();
        }
    }

    public void showNum()
    {
        keyboard_num.SetActive(true);
        keyboard_symbol.SetActive(false);
        keyboard_enUp.SetActive(false);
        keyboard_enLow.SetActive(false);
    }

    public void showSymbol()
    {
        keyboard_num.SetActive(false);
        keyboard_symbol.SetActive(true);
        keyboard_enUp.SetActive(false);
        keyboard_enLow.SetActive(false);
    }

    public void showEnUp()
    {
        keyboard_num.SetActive(false);
        keyboard_symbol.SetActive(false);
        keyboard_enUp.SetActive(true);
        keyboard_enLow.SetActive(false);
    }

    public void showEnLow()
    {
        keyboard_num.SetActive(false);
        keyboard_symbol.SetActive(false);
        keyboard_enUp.SetActive(false);
        keyboard_enLow.SetActive(true);
    }

    private void setTextString()
    {
        string text = "";
        for (int i = 0, l = str.Count; i < l; i++)
        {
            text += str[i];
        }
        input.text = text;
    }

    private void InitTween()
    {
        TweenBase[] tb = GetComponentsInChildren<TweenBase>();
        for (int i = 0; i < tb.Length; i++)
        {
            mlist.Add(tb[i]);
        }
    }

    private List<TweenBase> mlist = new List<TweenBase>();
    private void Begin()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }
        for (int i = 0; i < mlist.Count; i++)
        {
            mlist[i].Init();
            if (mlist[i].delaytime > 0)
            {
                StartCoroutine(DelayStart(mlist[i].delaytime, mlist[i]));
            }
            else
            {
                mlist[i].StartAction();
            }

        }
    }

    IEnumerator DelayStart(float time, TweenBase tb)
    {
        yield return new WaitForSeconds(time);
        tb.StartAction();
    }
}
