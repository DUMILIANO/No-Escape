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
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && doOnce)
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

