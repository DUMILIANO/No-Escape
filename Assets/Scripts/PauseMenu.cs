using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public class PauseMenu : MonoBehaviour
    {

        public static bool GameIsPaused = false;
        public GameObject PauseMenuUI;
        public InventoryUI inventoryUi;
        public GameObject PausedMenu;
        public GameObject SettingsMenu;
        public GameObject AudioMenu;
        public GameObject ControlsMenu;
        public GameObject DisplayMenu;
        public GameObject Inventory;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (GameIsPaused)
                {
                    Resume();

                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            inventoryUi.cursorIsLocked = true;
            PausedMenu.SetActive(false);
            ControlsMenu.SetActive(false); 
            DisplayMenu.SetActive(false); 
            AudioMenu.SetActive(false); 
            SettingsMenu.SetActive(false);
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            inventoryUi.cursorIsLocked = false;
            Inventory.SetActive(false);
            PausedMenu.SetActive(true);
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    

        public void QuitGame()
        {
            Application.Quit();

        }
    }

}
