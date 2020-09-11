using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBaseConfig : BaseConfig
{
    public TaskBaseConfig() {
        configType = ConfigType.TASK;
    }
}
