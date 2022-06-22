using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Checkpoint : MonoBehaviour
    {
        private GameMaster gm;

        void Start()
        {  
            gm = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gm.lastCheckPointPos = transform.position;
            }

        }
    }
}