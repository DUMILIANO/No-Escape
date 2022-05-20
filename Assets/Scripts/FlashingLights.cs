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
        public List<GameObject> lights = new List<GameObject>();
        public GameObject lamp;
        public Material lanternMaterial;
        public Material[] mat;
        public Renderer rend;
        public int lightNumber;
        public LightController control;

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
            timeDelay = Random.Range(0.01f, 0.3f);
            rend.materials[lightNumber].color = mat[0].color;
            rend.materials[lightNumber].DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(timeDelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.3f);
            rend.materials[lightNumber].color = mat[1].color;
            rend.materials[lightNumber].EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(timeDelay);
            isFlickering = false;
            insideCol = true;

            
            
        }

        IEnumerator GoOff()
        {
            yield return new WaitForSeconds(5f);
            isFlickering = true;
            this.gameObject.GetComponent<Light>().enabled = false;
            control.Source.Pause();
            rend.materials[lightNumber].color = mat[0].color;
            rend.materials[lightNumber].DisableKeyword("_EMISSION");

        }
    }
}
