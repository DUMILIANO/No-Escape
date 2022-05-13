using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class OpenStorage : MonoBehaviour
    {
        public doorController door;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player")
            {
                door.locked = false;
            }
        }
    }

}
