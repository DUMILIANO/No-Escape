using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        Item item;
        public InventoryUI invUi;

        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }

        public void ClearSlot ()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void UseItem()
        {
            if(item != null)
            {
                item.Use();
                invUi.inventoryUI.SetActive(false);
                invUi.cursorIsLocked = true;
            }
        }
    }

}
