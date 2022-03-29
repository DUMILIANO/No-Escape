using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace scripts
{
    public class Rotatelock : MonoBehaviour
    {

        public static event UnityAction<string, int> Rotated = delegate { };
        private bool coroutineAllowed;
        private int numberShown;
        public InventoryUI inventoryUI;
        public bool pressed = false;

        float time = 0;

        int position = 0;
        float space = 0f;

        // Start is called before the first frame update
        void Start()
        {
            coroutineAllowed = true;
            numberShown = 5;
        }
        public void OnMouseDown()
        {
            position++;
            space = space + 32f;
        }

        void Update() {
            if (position == 0)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 0
            } else if (position == 1)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 2)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 3)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 4)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 5)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 6)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 7)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 8)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 9)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }
            else if (position == 10)
            {
                if (transform.rotation.eulerAngles.z != space)
                {
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, space), Time.deltaTime * 5);
                }
                //Set rotation to pos 1
            }

            if(position == 10)
            {
                position = 0;
            }

        }
    }
}


