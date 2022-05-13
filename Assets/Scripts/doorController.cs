using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts
{
    public class doorController : MonoBehaviour
    {
        public Animator doorAnim;
        public bool open = false;
        public bool locked = false;
        public bool isRedDoor;
        public bool isWhiteDoor;    
        public GameObject key;
        public string openAnimationName;
        public string closeAnimationName;
        public AudioClip doorOpeningSFX;
        public AudioClip doorLockedSFX;
        public AudioSource audio;
        public bool doOnce = false;

        private void Awake()
        {
            //doorAnim = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            if(!locked)
            {
                key = null;
            }
        }
        public void PlayAnimation()
        {
            if (!locked && !open)
            {
                doorAnim.Play(openAnimationName, 0, 0.0f);
                //open = true;
            }
            else if (open)
            {
                doorAnim.Play(closeAnimationName, 0, 0.0f);
                //open = false;
            }
        }
        public void CheckOpen()
        {
            open = true;
            doOnce = !doOnce;
        }
        public void CheckClose()
        {
            open = false;
            doOnce = !doOnce;
        }
}
}

