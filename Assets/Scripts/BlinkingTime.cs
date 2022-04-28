using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Scripts
{
    public class BlinkingTime : MonoBehaviour
    {
        public TMP_Text microwaveTxt;
        public bool textOn = true;

        // Update is called once per frame
        void Update()
        {
            if(textOn == true)
            {
                microwaveTxt.enabled = false;
            }
            
            if(textOn == false)
            {
                microwaveTxt.enabled = true;
            }
        }
    }


}
