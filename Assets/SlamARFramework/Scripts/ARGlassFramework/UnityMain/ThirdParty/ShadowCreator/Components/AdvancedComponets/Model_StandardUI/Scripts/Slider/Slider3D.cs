using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SC {
    public class Slider3D : PointEffectBase
    {
        public float value = 0;

        public Transform frontGround;
        public Transform backGround;

        private float initfrontScale;
        private float initbackScale;

        protected override void Awake() {
            base.Awake();
            initbackScale = backGround.localScale.z;
            initfrontScale = frontGround.localScale.z;
        }

        protected override void Update() {
            base.Update();

            if (value <= 0) {
                value = 0;
            } else if (value >= 1) {
                value = 1;
            }

            frontGround.localScale = new Vector3(frontGround.localScale.x, frontGround.localScale.y,value * initbackScale);


        }
    }
}

