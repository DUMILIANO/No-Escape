using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Scripts
{
    public class jumpscareAnim : MonoBehaviour
    {
        public movementAI movementAI;
        public GameObject wspp;
        public AudioSource audio;
        public AudioClip ambience, lights, scream;
        public GameObject jumpPanel;
        public InventoryUI inventoryUI;
        public FirstPersonController fps;
        public PhoneUI phone;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(deathScene());
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public IEnumerator deathScene()
        {
            if(phone.cameraOn == false)
            {
                fps.enabled = false;
                wspp.SetActive(false);
                audio.PlayOneShot(ambience);
                audio.PlayOneShot(lights);
                jumpPanel.SetActive(true);
                audio.PlayOneShot(scream);
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene(2);
                inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
            }
            else
            {
                StartCoroutine(phone.FinishAnim());
                fps.enabled = false;
                wspp.SetActive(false);
                audio.PlayOneShot(ambience);
                audio.PlayOneShot(lights);
                jumpPanel.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                audio.PlayOneShot(scream);
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene(2);
                inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
            }
            
        }
    }
    

}
