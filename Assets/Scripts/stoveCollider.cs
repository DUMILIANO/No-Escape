using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace Scripts
{
    public class stoveCollider : MonoBehaviour
    {
        public TMP_Text stoveTxt;
        bool doOnce = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && doOnce)
            {
                stoveTxt.gameObject.SetActive(true);
                StartCoroutine(textOff());
                doOnce = false;
            }
        }

        IEnumerator textOff()
        {
            yield return new WaitForSeconds(2);
            stoveTxt.gameObject.SetActive(false);

        }
    }
}
