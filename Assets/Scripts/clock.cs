using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
    {
        public class clock : MonoBehaviour
        {
            public float hourSpace;
            public float minuteSpace;
            public GameObject hourHand;
            public GameObject minuteHand;
            public doorController door;


            void Start()
            {
                hourSpace = 45f;
                minuteSpace = 11f;
            }

            void Update()
            {
                
                
                if((Mathf.Round(minuteHand.transform.rotation.eulerAngles.x * 2) / 2) == 35 && (Mathf.Round(hourHand.transform.rotation.eulerAngles.x * 2) / 2) == 15)
                {
                    door.locked = false;

                }
            }
            public void hours()
            {
                hourSpace = hourSpace + 30f;
                hourHand.transform.rotation = Quaternion.Euler(hourSpace, -90f, 90f);
                Debug.Log(hourHand.transform.rotation.eulerAngles.x);
            }
            public void minutes()
            {
                minuteSpace = minuteSpace + 6f;
                minuteHand.transform.rotation = Quaternion.Euler(minuteSpace, -90f, 90f);
                Debug.Log(minuteHand.transform.rotation.eulerAngles.x);
            }
        }
    }
