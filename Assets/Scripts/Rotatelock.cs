using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace Scripts
{
    public class Rotatelock : MonoBehaviour
    {

        public static event UnityAction<string, int> Rotated = delegate { };
        private bool coroutineAllowed;
        private int numberShown;
        public InventoryUI inventoryUI;
        public bool pressed = false;
        public float rotateSpeed = 2.719166f;
        bool inLockView = false;
        public Raycast raycast;
        public string name;


        // Start is called before the first frame update
        void Start()
        {
            coroutineAllowed = true;
            numberShown = 10;
        }
        public void OnMouseDown()
        {
            if (coroutineAllowed)
            {
                StartCoroutine(RotateWheel());
            }

        }


        private void Update()
        {

            if (raycast.inLockView == true && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed");
                raycast.crosshair.enabled = true;
                raycast.picktxt.enabled = true;
                raycast.lockCam.SetActive(false);
                raycast.player.SetActive(true);
                inventoryUI.cursorIsLocked = true;
                raycast.lockObject.SetActive(false);
                raycast.leaveLockTxt.gameObject.SetActive(false);

            }

        }


        IEnumerator RotateWheel()
        {
           
            coroutineAllowed = false;

            for (int i = 0; i <= 11; i++)
            {              
                transform.Rotate(Vector3.forward * rotateSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            coroutineAllowed = true;

            numberShown += 1;

            if (numberShown > 10)
            {
                numberShown = 0;
            }

            Rotated(name, numberShown);
        }
    }
}


