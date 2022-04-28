using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public bool isFlickering;
        public bookChecker pC;
        public float timeDelay;
        public bool lightsOn = true;
        public bool insideCol = false;
        public bool finished = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && pC.puzzleComplete == true && finished == false)
            {
                Debug.Log("working");
                isFlickering = false;
                finished = true;
            }

        }

        void Update()
        {
            if (isFlickering == false)
            {
                StartCoroutine(FlickeringLight());
            }
            else if (insideCol == true)
            {
                StartCoroutine(GoOff());
            }
        }

        IEnumerator FlickeringLight()
        {
            isFlickering = true;
            this.gameObject.GetComponent<Light>().enabled = false;
            timeDelay = Random.Range(0.01f, 0.1f);
            yield return new WaitForSeconds(timeDelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.1f);
            yield return new WaitForSeconds(timeDelay);
            isFlickering = false;
            insideCol = true;
        }

        IEnumerator GoOff()
        {
            yield return new WaitForSeconds(3f);
            isFlickering = true;
            this.gameObject.GetComponent<Light>().enabled = false;
        }
    }
}
