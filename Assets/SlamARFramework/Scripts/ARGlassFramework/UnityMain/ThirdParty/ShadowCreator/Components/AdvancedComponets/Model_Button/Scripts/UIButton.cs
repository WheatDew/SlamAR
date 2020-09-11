using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace SC
{
	[AddComponentMenu("ShadowCreator/UIButton")]
	public class UIButton : Button{


        public static AudioClip clickAudio;
        public static AudioClip enterAudio;

        private AudioSource mAudioSource;
        enum AudioType {
            Enter,
            Click,
        }

        protected override void Awake() {
            base.Awake();
            AddBoxCollider();
            clickAudio = Resources.Load<AudioClip>("Sound/btnSound");
            enterAudio = Resources.Load<AudioClip>("Sound/mouseOver");
            mAudioSource = GetComponent<AudioSource>();
            if(!mAudioSource) {
                mAudioSource = gameObject.AddComponent<AudioSource>();
                mAudioSource.playOnAwake = false;
            }
        }

        void AddBoxCollider() {
            RectTransform rt = GetComponent<RectTransform>();
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if(rt) {
                if(boxCollider == null) {
                    boxCollider = gameObject.AddComponent<BoxCollider>();
                }
                boxCollider.size = new Vector3(rt.sizeDelta.x, rt.sizeDelta.y, 0);
            }
        }

        void PlayAudio( AudioType type ) {
            if(type == AudioType.Click && clickAudio) {
                mAudioSource.clip = clickAudio;
                mAudioSource.Play();
            } else if(type == AudioType.Enter && enterAudio) {
                mAudioSource.clip = enterAudio;
                mAudioSource.Play();
            }
        }

        public override void OnPointerClick( PointerEventData eventData ) {
            base.OnPointerClick(eventData);
            PlayAudio(AudioType.Click);
        }




    }
}

