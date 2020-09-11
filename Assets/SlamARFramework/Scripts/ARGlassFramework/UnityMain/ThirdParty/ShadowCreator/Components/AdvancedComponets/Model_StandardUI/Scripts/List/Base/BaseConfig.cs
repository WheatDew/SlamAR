using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BaseConfig
{
    /// <summary>
    /// BaseView的Title
    /// </summary>
    public string title = "a title";

    /// <summary>
    /// BaseView的简介
    /// </summary>
    public string introduction = "an introduction";

    public enum ConfigType {
        TASK,
        PROJECT
    }

    public ConfigType configType;

    public virtual void OnClick() {

    }
    public virtual void OnEnter() {

    }
    public virtual void OnExit() {

    }
    public virtual void OnDown() {

    }
    public virtual void OnUp() {

    }



    public virtual void Start() {
    }

    public virtual void Awake() {

    }

    public virtual void Update() {
    }

    public virtual void OnDestroy() {

    }

    public virtual void Init() {

    }

}
