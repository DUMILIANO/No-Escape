using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class ClockCollider : MonoBehaviour
    {
        public TMP_Text clockTxt;
        public bookChecker complete;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && complete.puzzleComplete == true)
            {
                clockTxt.gameObject.SetActive(true);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && complete.puzzleComplete == true)
            {
                clockTxt.gameObject.SetActive(false);

            }
        }
    }


}

