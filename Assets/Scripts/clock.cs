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
            public float rotateSpeed;


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
                //hourSpace = hourSpace + 30f;
                Quaternion hourCurrentRot = hourHand.transform.rotation;
                Quaternion hourTargerRot = Quaternion.Euler(hourSpace, -90f, 90f);
                hourHand.transform.rotation = Quaternion.Slerp(hourCurrentRot, hourTargerRot, rotateSpeed);

                Quaternion minuteCurrentRot = minuteHand.transform.rotation;
                Quaternion minuteTargerRot = Quaternion.Euler(minuteSpace, -90f, 90f);
                minuteHand.transform.rotation = Quaternion.Slerp(minuteCurrentRot, minuteTargerRot, rotateSpeed);
            }
            public void hours()
            {
                hourSpace = hourSpace + 30f;
                /*Quaternion currentRot = hourHand.transform.rotation;
                Quaternion targerRot = Quaternion.Euler(hourSpace, -90f, 90f);
                hourHand.transform.rotation = Quaternion.Slerp(currentRot, targerRot, rotateSpeed);*/
                //hourHand.transform.rotation = Quaternion.Euler(hourSpace, -90f, 90f);
                
            }
            public void minutes()
            {
                minuteSpace = minuteSpace + 6f;
                //minuteHand.transform.rotation = Quaternion.Euler(minuteSpace, -90f, 90f);
            }
        }
    }
