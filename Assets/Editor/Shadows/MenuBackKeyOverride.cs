using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace SC.Menu
{
	public class MenuBackKeyOverride : MenuBase {

        [MenuItem("GameObject/ShadowCreator/BackKeyOverride", false, 81)]
        public static void MenuItemSCButton() {
            CreatePrefab("Prefabs/BackKeyOverride");
        }
    }
}
