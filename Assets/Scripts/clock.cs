using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
    {
        public class clock : MonoBehaviour
        {

            void Start()
            {
                
            }

            void Update()
            {
                
            }
            public void hours()
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x + 25f, -90f, 90f);
                Debug.Log("done");
            }
        }
    }
