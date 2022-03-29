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
        // Start is called before the first frame update
        void Start()
        {
            coroutineAllowed = true;
            numberShown = 5;
        }
        public void OnMouseDown()
        {
            Debug.Log("Running");


            if (coroutineAllowed)
            {
                StartCoroutine(RotateWheel());
            }

        }



        IEnumerator SlowRotation()
        {

        }



        IEnumerator RotateWheel()
        {
            coroutineAllowed = false;

            for (int i = 0; i <= 11; i++)
            {
                transform.Rotate(0f, 0f, -32.72f);
                yield return new WaitForSeconds(0.01f);
            }

            coroutineAllowed = true;

            numberShown += 1;

            if (numberShown > 9)
            {
                numberShown = 0;
            }

            Rotated(name, numberShown);
        }
    }
}


