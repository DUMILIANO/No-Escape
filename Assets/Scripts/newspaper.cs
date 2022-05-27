using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class newspaper : MonoBehaviour
    {
        public TMP_Text newspaperTxt;

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                newspaperTxt.gameObject.SetActive(true);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if(other.tag == "Player")
            {
                newspaperTxt.gameObject.SetActive(false);
            }
        }
    }
}



