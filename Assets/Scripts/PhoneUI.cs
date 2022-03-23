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
        public GameObject[] objects;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
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
        void OnGUI()
        {
            foreach (GameObject go in objects)
            {
                bool active = GUILayout.Toggle(go.activeSelf, go.name);
                if (active != go.activeSelf)
                {
                    go.SetActive(active);
                }
            }
        }
    }

}

