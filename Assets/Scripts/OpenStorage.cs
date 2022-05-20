using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class OpenStorage : MonoBehaviour
    {
        public doorController door;
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player")
            {
                door.locked = false;
            }
        }
    }

}
