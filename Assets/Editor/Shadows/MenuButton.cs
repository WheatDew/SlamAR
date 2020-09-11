using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace SC.Menu
{
	public class MenuButton : MenuBase {

        [MenuItem("GameObject/ShadowCreator/SCButton", false, 51)]
        public static void MenuItemSCButton() {
            CreatePrefab("Prefabs/SCButton");
        }

        [MenuItem("GameObject/ShadowCreator/UIButton", false, 50)]
        public static void MenuItemUIButton() {
            CreatePrefab("Prefabs/UICanvas");
        }
    }
}
