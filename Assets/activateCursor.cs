using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class activateCursor : MonoBehaviour
    {
        public InventoryUI inventoryUI;
        // Start is called before the first frame update
        void Awake()
        {
            inventoryUI.cursorIsLocked = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

