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
        public Renderer myRenderer;
        bool cameraOn = false;
        public GameObject recordingUI;

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
                recordingUI.SetActive(true);
                myRenderer.enabled = false;
            }
            else
            {
                cameraOn = false;
                recordingUI.SetActive(false);
                myCamera.fieldOfView = 70f;
                objects.SetActive(false);
                PostPro.SetActive(false);
                myRenderer.enabled = true;
            }

            
            
        }
    }

}

