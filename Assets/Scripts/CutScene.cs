using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

namespace Scripts
{
    public class CutScene : MonoBehaviour
    {
        public new BoxCollider collider;
        public Animation gCutscene;
        public bool animPlayed = false;
        public GameObject player;
        public Image crosshair;
        public bookChecker puzzleDone;
        public GameObject panel;
        public GameObject PostPro;
        public Camera myCamera;
        public GameObject recordingUI;
        public Renderer phoneRenderer;
        public BoxCollider phoneCollider;
        public GameObject enemy;
        public GameObject phone;
        public GameObject FirstPersonController;



        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && animPlayed == false && puzzleDone.puzzleComplete == true)
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                crosshair.enabled = false;
                gCutscene.Play();
                animPlayed = true;
                StartCoroutine(PhoneOnAnimation());



            }
        }

        IEnumerator Fade()
        {
            panel.GetComponent<Animation>().Play("FadeIn");
            yield return new WaitForSeconds(1);
            panel.GetComponent<Animation>().Play("FadeOut");
        }

        IEnumerator PhoneOnAnimation()
        {
            yield return new WaitForSeconds(0.2f);
            phone.GetComponent<Animation>().Play("PhoneOnCutScene");
            yield return new WaitForSeconds(1f);
            StartCoroutine(Fade());
            yield return new WaitForSeconds(0.9f);
            myCamera.fieldOfView = 50f;
            PostPro.SetActive(true);
            recordingUI.SetActive(true);
            phoneRenderer.enabled = false;
            phoneCollider.enabled = false;
            enemy.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(1);
            StartCoroutine(PhoneOffAnimation());
        }

        IEnumerator PhoneOffAnimation()
        {
            StartCoroutine(Fade());
            yield return new WaitForSeconds(1f);
            FirstPersonController.GetComponent<Animation>().Play("GhostCutSceneOff");
            phone.GetComponent<Animation>().Play("PhoneOffCutscene");
            phoneRenderer.enabled = true;
            myCamera.fieldOfView = 70f;
            PostPro.SetActive(false);
            recordingUI.SetActive(false);
            yield return new WaitForSeconds(0.01f);
            phoneCollider.enabled = true;
            enemy.GetComponent<Renderer>().enabled = false;
            player.GetComponent<FirstPersonController>().enabled = true;
            crosshair.enabled = true;

        }
    }
}