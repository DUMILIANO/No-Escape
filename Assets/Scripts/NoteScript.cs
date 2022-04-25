using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class NoteScript : MonoBehaviour
    {
        public Raycast raycast;
        public InventoryUI inventoryUI;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void Update()
        {

            if (raycast.inNoteView == true && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed");
                raycast.crosshair.enabled = true;
                raycast.picktxt.enabled = true;
                raycast.note.SetActive(false);
                raycast.player.SetActive(true);
                inventoryUI.cursorIsLocked = true;
            
            }

        }
    }

}
