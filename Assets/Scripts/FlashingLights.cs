using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public bool isFlickering = false;
        public float timeDelay;

        /*void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (isFlickering == false)
                {
                    StartCoroutine(FlickeringLight());
                }
            }
               
        }*/

        void Update()
        {
            if (isFlickering == false)
            {
                StartCoroutine(FlickeringLight());
            }

        }

        IEnumerator FlickeringLight()
        {
            isFlickering = true;
            this.gameObject.GetComponent<Light>().enabled = false;
            timeDelay = Random.Range(0.01f, 0.3f);
            yield return new WaitForSeconds(timeDelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.3f);
            yield return new WaitForSeconds(timeDelay);
            isFlickering = false;
        }
    }
}

