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
        public bool blinking;

        // Update is called once per frame
        void Update()
        {
            if(blinking == false)
            {
                StartCoroutine(blink());
            }
        }

        IEnumerator blink()
        {
            blinking = true;
            microwaveTxt.enabled = false;
            yield return new WaitForSeconds(0.1f);
            microwaveTxt.enabled = true;
            yield return new WaitForSeconds(0.1f);
            blinking = false;
        }
    }


}
