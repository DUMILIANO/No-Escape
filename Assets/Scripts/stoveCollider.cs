using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace Scripts
{
    public class stoveCollider : MonoBehaviour
    {
        public TMP_Text stoveTxt;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                stoveTxt.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                stoveTxt.gameObject.SetActive(false);
            }
        }
    }
}
