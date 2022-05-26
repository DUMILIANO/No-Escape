using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class ClockCollider : MonoBehaviour
    {
        public TMP_Text clockTxt;
        bool doOnce = true;
        public bookChecker complete;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && doOnce && complete.puzzleComplete == true)
            {
                clockTxt.gameObject.SetActive(true);
                StartCoroutine(textOff());
                doOnce = false;
            }
        }

        IEnumerator textOff()
        {
            yield return new WaitForSeconds(2);
            clockTxt.gameObject.SetActive(false);

        }
    }


}

