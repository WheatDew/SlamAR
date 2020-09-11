using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectBaseConfig : BaseConfig
{

    protected BaseList baseList;
    protected List<BaseConfig> configs;

    public ProjectBaseConfig(BaseList baseList, List<BaseConfig> configs) {
        configType = ConfigType.PROJECT;

        this.baseList = baseList;
        this.configs = configs;
    }

    public override void Init() {
        base.Init();
        BaseListUpdateConfig();
    }

    public override void OnClick() {
        base.OnClick();
        BaseListUpdateConfig();
    }

    void BaseListUpdateConfig() {
        if (!baseList) {
            Debug.LogError("Please Init baseList");
            return;
        }
        if (null == configs) {
            Debug.LogError("Please Init configs");
            return;
        }
        baseList.UpdateConfigs(configs);
    }
}
