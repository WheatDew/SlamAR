using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace SC.Menu
{
	public class MenuInputField3D : MenuBase {

        [MenuItem("GameObject/ShadowCreator/InputField3D", false, 61)]
        public static void MenuItemSCButton() {
            CreatePrefab("Prefabs/InputField3D");
        }
    }
}
