using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts
{
    public class basementLight : MonoBehaviour
    {
        public float timeDelay;
        public Material[] mat;
        public Renderer rend;
        public int lightNumber;
        // Start is called before the first frame update
        void Update()
        {
            StartCoroutine(FlickeringLight());
        }

        IEnumerator FlickeringLight()
        {
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
        }
    }
}

