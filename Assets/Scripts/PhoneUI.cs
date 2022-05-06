using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class PhoneUI : MonoBehaviour
    {
        public GameObject flashlight;
        public InventoryUI inventoryUI;
        public GameObject phoneUI;
        public GameObject objects;
        public GameObject phone;
        public GameObject PostPro;
        public GameObject panel;
        public Camera myCamera;
        public Renderer myRenderer;
        public BoxCollider phoneCollider;
        public bool cameraOn = false;
        public GameObject recordingUI;
        public AudioSource phoneturnOn;
        public GameObject enemy;
        public TMP_Text phoneText;
        public Raycast raycast;
        public movementAI AI;
        public GameObject ghostAI;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.X) && GameObject.Find("phone").GetComponent<PickUp>().equipped)
            {
                raycast.phoneText.gameObject.SetActive(false);
                raycast.objectives.gameObject.SetActive(false);
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
            StartCoroutine(FinishAnim());
            if(AI.hittable == true && cameraOn == false)
            {
                var rand = new Vector3(Random.Range(25f, 54f), ghostAI.transform.position.y, Random.Range(-1f, -24f));
                ghostAI.transform.position = rand;
                StartCoroutine(AIspeed());
            }

        }

        IEnumerator Fade()
        {
            panel.GetComponent<Animation>().Play("FadeIn");
            yield return new WaitForSeconds(1);
            panel.GetComponent<Animation>().Play("FadeOut");
        }

        public void playSoundEffects()
        {
            phoneturnOn.Play();
        }

        IEnumerator FinishAnim()
        {
            phoneUI.SetActive(false); 
            inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
            StartCoroutine(Fade());  

            if (cameraOn == false)
            {
                phone.GetComponent<Animation>().Play("phone");         
                yield return new WaitForSeconds(0.9f);
                cameraOn = true;
                myCamera.fieldOfView = 50f;
                objects.SetActive(true);
                PostPro.SetActive(true);
                recordingUI.SetActive(true);
                myRenderer.enabled = false;
                phoneCollider.enabled = false;
                enemy.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(1);
                phone.GetComponent<Animation>().Play("phoneback");
                Debug.Log("AnimationPlaying");
                myRenderer.enabled = true;
                myCamera.fieldOfView = 70f;
                PostPro.SetActive(false);
                recordingUI.SetActive(false);
                yield return new WaitForSeconds(0.01f);
                cameraOn = false;
                objects.SetActive(false);
                phoneCollider.enabled = true;
                enemy.SetActive(false);

            }


        }
        IEnumerator AIspeed()
        {
            AI.enemy.speed = 0f;
            yield return new WaitForSeconds(5f);
            AI.enemy.speed = 3f;
        }
    }

}

