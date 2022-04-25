using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public bool isFlickering;
        public bookChecker puzzleComplete;
        public float timeDelay;
        public bool lightsOn = true;
        public bool insideCol = false;
        float timer = 0f;
        float waitTime = 3f;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && puzzleComplete == true)
            {
                isFlickering = false;
                insideCol = true;
     
    }

        }

        void Update()
        {

            if (insideCol == true)
            {

                StartCoroutine(GoOff());
                /*timer += Time.deltaTime;
                Debug.Log(timer);
                if(timer > waitTime)
                {
                    lightsOn = false;
                    timer = timer - waitTime;
                    this.gameObject.GetComponent<Light>().enabled = false;
                }*/
            }
            if (isFlickering == false)
            {

                StartCoroutine(FlickeringLight());

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
        }

        IEnumerator GoOff()
        {
            yield return new WaitForSeconds(3f);
            this.gameObject.GetComponent<Light>().enabled = false;
        }
    }
}
