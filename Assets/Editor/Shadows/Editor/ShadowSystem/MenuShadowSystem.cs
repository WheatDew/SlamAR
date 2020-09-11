using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SC.Menu {
    public class MenuShadowSystem : MenuBase {

        [MenuItem("GameObject/ShadowCreator/ShadowSystem", false, 10)]
        public static void createAction() {
            CreatePrefab("Prefabs/ShadowSystem");
        }

        [MenuItem("ShadowCreator/ShadowSystem", false, 10)]

        public static void createAction1() {
            CreatePrefab("Prefabs/ShadowSystem");
        }

    }
}
