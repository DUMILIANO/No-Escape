using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public bool isFlickering;
        public float timeDelay;
        public bool lightsOn;
        public bool insideCol = false;
        float timer;
        float waitTime;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isFlickering = false;
                insideCol = true;
                
            }
     
        }

        void Update()
        {
            
            if(insideCol)
            {
                timer += Time.deltaTime;
                if(timer > waitTime)
                {
                    lightsOn = false;
                    timer = timer - waitTime;
                }
            }
            if (isFlickering == false && lightsOn)
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
    }
}

