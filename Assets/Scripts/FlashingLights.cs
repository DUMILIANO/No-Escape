using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public bool isFlickering;
        public bookChecker pC;
        public float timeDelay;
        public bool lightsOn = true;
        public bool insideCol = false;
        public bool finished = false;
        public List<GameObject> lights = new List<GameObject>();
        //public Material lampOffMaterial;
        //public Material lampOnMaterial;
        public GameObject lamp;
        public Material lanternMaterial;
        public Material[] mat;


        void Start()
        {
            mat = lamp.GetComponent<Renderer>().materials;
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
            lamp.GetComponent<Renderer>().materials[1] = lanternMaterial;
            Debug.Log(lamp.GetComponent<Renderer>().materials[1]);

        }
    }
}
