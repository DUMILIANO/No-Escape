using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts
{
    public class drawersAnim : MonoBehaviour
    {
        public Animator drawerAnim;
        private bool open = false;
        public bool doOnce = false;

        public AudioSource audio;
        public AudioClip openingSFX;
        public AudioClip closingSFX;

        public void PlayAnimation()
        {
            if (!open)
            {
                drawerAnim.Play("drawerOpen", 0, 0.0f);
                audio.PlayOneShot(openingSFX);
                open = true;
            }
            else if (open)
            {
                drawerAnim.Play("drawerClose", 0, 0.0f);
                audio.PlayOneShot(closingSFX);
                open = false;
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