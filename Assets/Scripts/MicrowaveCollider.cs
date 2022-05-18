using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Scripts
{
    public class MicrowaveCollider : MonoBehaviour
    {
        public TMP_Text timeTxt;
        bool doOnce = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && doOnce)
            {
                timeTxt.gameObject.SetActive(true);
                StartCoroutine(textOff());
                doOnce = false;
            }
        }

        IEnumerator textOff()
        {
            yield return new WaitForSeconds(2);
            timeTxt.gameObject.SetActive(false);

        }
    }
}

