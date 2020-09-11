using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseList : BaseWindow {

    private BaseView[] _baseViewList;
    public BaseView[] BaseViewList {
        get {
            if (_baseViewList==null) {
                _baseViewList = GetComponentsInChildren<BaseView>();
            }
            return _baseViewList;
        }
    }

    protected List<BaseConfig> configs;
    public int SumPage { get; set; }
    public int CurrentPage { get; set; }

    public int CurrentViewNum=1;

    [Header("Can Emplty")]
    public SCButton preButton;
    [Header("Can Emplty")]
    public SCButton nextButton;
    [Header("Can Emplty")]
    public TextMesh title;
    [Header("Can Emplty")]
    public TextMesh pageNum;
    [Header("Can Emplty")]
    public Mark marks;
    
    void UpdateButtonsStatus() {
        if (SumPage == 1) {
            if (nextButton && nextButton.gameObject.activeSelf == true) {
                nextButton.gameObject.SetActive(false);
            }
            if (preButton && preButton.gameObject.activeSelf == true) {
                preButton.gameObject.SetActive(false);
            }
        } else if (CurrentPage == SumPage) {
            if (nextButton && nextButton.gameObject.activeSelf == true) {
                nextButton.gameObject.SetActive(false);
            }
            if (preButton && preButton.gameObject.activeSelf == false) {
                preButton.gameObject.SetActive(true);
            }
        } else if (CurrentPage == 1) {
            if (nextButton && nextButton.gameObject.activeSelf == false) {
                nextButton.gameObject.SetActive(true);
            }
            if (preButton && preButton.gameObject.activeSelf == true) {
                preButton.gameObject.SetActive(false);
            }
        } else {
            if (nextButton && nextButton.gameObject.activeSelf == false) {
                nextButton.gameObject.SetActive(true);
            }
            if (preButton && preButton.gameObject.activeSelf == false) {
                preButton.gameObject.SetActive(true);
            }
        }
    }

    public override void Awake() {
        base.Awake();
        SCButtonAddListener();
    }

    public void SetTitle(string title) {
        if (this.title) {
            this.title.text = title;
        }
    }

    public virtual void UpdateConfigs(List<BaseConfig> configs) {
        if (configs == null) {
            configs = new List<BaseConfig>();
        }
        this.configs = configs;
        try {
            configs[CurrentViewNum-1].Init();
        } catch (Exception e) {
            Debug.Log(e);
        }

        ///重新计算SumPage
        int aPageMaxView = BaseViewList.Length;
        int sumConfigCount = configs.Count;
        SumPage = sumConfigCount / aPageMaxView + (((sumConfigCount % aPageMaxView) != 0) ? 1 : 0);
        if (SumPage == 0)
            SumPage = 1;
        CurrentPage = 1;
        //Debug.Log("sumConfigCount:"+ sumConfigCount +" UpdateConfigs:" +SumPage);
        Refresh();
    }

    protected virtual void Refresh() {

        if (configs == null)
            return;
        ///Reset BaseView
        foreach (BaseView item in BaseViewList) {
            item.gameObject.SetActive(false);
            item.UpdateConfig(null);
        }
        ///填充BaseView
        for (int i = (CurrentPage - 1)* BaseViewList.Length, j = 0; j < ((CurrentPage == SumPage) ? (configs.Count - BaseViewList.Length * (CurrentPage - 1)) : BaseViewList.Length) ; i++,j++) {
            BaseViewList[j].gameObject.SetActive(true);
            BaseViewList[j].UpdateConfig(configs[i]);
        }
        UpdateButtonsStatus();

        if (pageNum) {
            pageNum.text = CurrentPage + "/" + SumPage;
        }
        if(marks) {
            marks.RefreshMarks(SumPage, CurrentPage);
        }
        
    }

    public void SCButtonAddListener() {
        if (preButton) {
            preButton.onEnter.AddListener(PreButtonOnEnter);
            preButton.onClick.AddListener(PreButtonOnClick);
            preButton.onDown.AddListener(PreButtonOnDown);
            preButton.onUp.AddListener(PreButtonOnUp);
            preButton.onExit.AddListener(PreButtonOnExit);
        }
        if (nextButton) {
            nextButton.onEnter.AddListener(NextButtonOnEnter);
            nextButton.onClick.AddListener(NextButtonOnClick);
            nextButton.onDown.AddListener(NextButtonOnDown);
            nextButton.onUp.AddListener(NextButtonOnUp);
            nextButton.onExit.AddListener(NextButtonOnExit);
        }
        
    }
    public virtual void NextButtonOnEnter() {

    }
    public virtual void NextButtonOnDown() {

    }
    public virtual void NextButtonOnUp() {
        CurrentPage = (CurrentPage + 1) > SumPage ? SumPage : (CurrentPage + 1);
        Refresh();
    }
    public virtual void NextButtonOnExit() {

    }
    public virtual void NextButtonOnClick() {

    }


    public virtual void PreButtonOnEnter() {
    }
    public virtual void PreButtonOnDown() {

    }
    public virtual void PreButtonOnUp() {
        CurrentPage = (CurrentPage - 1) < 1 ? 1 : (CurrentPage - 1);
        Refresh();
    }
    public virtual void PreButtonOnExit() {

    }
    public virtual void PreButtonOnClick() {

    }
}
