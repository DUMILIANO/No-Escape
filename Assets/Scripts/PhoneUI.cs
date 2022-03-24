using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class PhoneUI : MonoBehaviour
    {
        public GameObject flashlight;
        public InventoryUI inventoryUI;
        public GameObject phoneUI;
        public GameObject objects;
        public GameObject phone;
        public GameObject PostPro;
        public Camera myCamera;
        bool cameraOn = false;

        //[SerializeField] public bool hasPhone = false;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.X) && GameObject.Find("phone").GetComponent<PickUp>().equipped)
            {
                phoneUI.SetActive(!phoneUI.activeSelf);
                inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
            }
        }
        public void UseFlashlight()
        {
            flashlight.SetActive(!flashlight.activeSelf);
            phoneUI.SetActive(false);
            inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
        }
        public void ShowObjects()
        {
            phoneUI.SetActive(false);
            inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;

            if (cameraOn == false)
            {
                cameraOn = true;
                myCamera.fieldOfView = 50f;
                objects.SetActive(true);
                PostPro.SetActive(true);
            }
            else
            {
                cameraOn = false;
                myCamera.fieldOfView = 70f;
                objects.SetActive(false);
                PostPro.SetActive(false);
            }

            
            
        }
    }

}

