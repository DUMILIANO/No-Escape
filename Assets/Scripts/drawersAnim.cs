using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts
{
    public class drawersAnim : MonoBehaviour
    {
        public Animator drawerAnim;
        private bool open = false;

        public void PlayAnimation()
        {
            if (!open)
            {
                drawerAnim.Play("drawerOpen", 0, 0.0f);
                open = true;
            }
            else if (open)
            {
                drawerAnim.Play("drawerClose", 0, 0.0f);
                open = false;
            }
        }
    }
}