using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
    {
        public class clock : MonoBehaviour
        {
            public float hourSpace;
            public float minuteSpace;


            void Start()
            {
                hourSpace = 45f;
                minuteSpace = 45f;
            }

            void Update()
            {
                
            }
            public void hours()
            {
                hourSpace = hourSpace + 30f;
                transform.rotation = Quaternion.Euler(hourSpace, -90f, 90f);
            }
            public void minutes()
            {
                minuteSpace = minuteSpace + 30f;
                transform.rotation = Quaternion.Euler(minuteSpace, -90f, 90f);
            }
        }
    }
