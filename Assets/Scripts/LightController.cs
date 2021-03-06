using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class LightController : MonoBehaviour
    {
        public bookChecker pC;
        public bool finished = false;
        public FlashingLights[] lights;
        public AudioSource Source;

        

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && pC.puzzleComplete == true && finished == false)
            {
                foreach (FlashingLights light in lights)
                {
                    light.isFlickering = false;
                    Source.Play();
                    finished = true;
                }
            }

        }
    }
}
