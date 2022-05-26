using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts
{
    public class closeLivingroom : MonoBehaviour
    {
        public doorController door;
        public bookChecker complete;
        public bool doOnce = true;

        public void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && complete.puzzleComplete == false && doOnce == true )
            {
                door.PlayAnimation();
                door.locked = true;
                doOnce = false;
            }

            else if(other.tag == "Player" && door.locked == true && complete.puzzleComplete == true)
            {
                door.locked = false;
            }
        }

    }
}
