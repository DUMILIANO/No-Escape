using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace scripts
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        Inventory inventory;
        InventorySlot[] slots;
        public GameObject inventoryUI;
        public bool cursorIsLocked = true;
        // Start is called before the first frame update
        void Start()
        {
            inventory = Inventory.instance;
            inventory.onItemChangeCallback += UpdateUI;
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                cursorIsLocked = !cursorIsLocked;
                
            }
            if(cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        void UpdateUI()
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}