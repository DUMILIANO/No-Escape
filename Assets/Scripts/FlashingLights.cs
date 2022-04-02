using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class FlashingLights : MonoBehaviour
    {
        public GameObject lights;
        float time;

        // Start is called before the first frame update
        void Start()
        { 
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Entered");
                for (int i = 2; i > Time.deltaTime;)
                {
                    Debug.Log(i);
                }

             
                //for (i = Time.deltaTime; )
                

                

            }

        }
    }
}

