using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Scripts
{
    public class MicrowaveCollider : MonoBehaviour
    {
        public TMP_Text timeTxt;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                timeTxt.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                timeTxt.gameObject.SetActive(false);
            }
        }

    }
}

