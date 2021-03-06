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
        public GameObject emissionPanel;
        public GameObject shadowPanel;
        public bool blinking;
        public bool CutSceneDone = false;
        public float timeDelay;
        public GameObject ghostAI;
        public PhoneUI menu;
        public GameObject Annie;
        public GameObject enemySkin;
        public GameObject closedPanel;
        public AudioSource ghostWhisper;
        public AudioSource ghostHeartbeat;
        public bool doOnce = false;


        public void Start()
        {
            if(doOnce == false)
            {
                player.GetComponent<Animation>().Play("WakingUp");
                doOnce = true;
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && animPlayed == false && puzzleDone.puzzleComplete == true)
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                crosshair.enabled = false;
                emissionPanel.SetActive(true);
                FirstPersonController.GetComponent<Animation>().Play("CameraXCutScene");
                gCutscene.Play();
                animPlayed = true;
                ghostWhisper.Play();
                ghostHeartbeat.Play();
                StartCoroutine(PhoneOnAnimation());
            }
        }

        public void Update()
        {

            if(blinking == false && puzzleDone.puzzleComplete == true && CutSceneDone == false)
            {
                StartCoroutine(blink());
            }
        }

        IEnumerator Fade()
        {
            panel.GetComponent<Animation>().Play("FadeIn");
            yield return new WaitForSeconds(1);
            panel.GetComponent<Animation>().Play("FadeOut");
        }

        IEnumerator blink()
        {
            blinking = true;
            emissionPanel.SetActive (false);
            yield return new WaitForSeconds(timeDelay);
            timeDelay = Random.Range(0.01f, 0.3f);
            emissionPanel.SetActive (true);
            yield return new WaitForSeconds(timeDelay);
            timeDelay = Random.Range(0.01f, 0.3f);
            blinking = false;
            emissionPanel.SetActive(false);
        }

        IEnumerator PhoneOnAnimation()
        {
            yield return new WaitForSeconds(0.2f);
            phone.GetComponent<Animation>().Play("phone");
            StartCoroutine(Fade());
            yield return new WaitForSeconds(0.9f);
            myCamera.fieldOfView = 30f;
            PostPro.SetActive(true);
            recordingUI.SetActive(true);
            phoneRenderer.enabled = false;
            phoneCollider.enabled = false;
            enemy.GetComponent<SkinnedMeshRenderer>().enabled = true;
            yield return new WaitForSeconds(3f);
            StartCoroutine(PhoneOffAnimation());
        }

        IEnumerator PhoneOffAnimation()
        {
            StartCoroutine(Fade());
            yield return new WaitForSeconds(1f);
            FirstPersonController.GetComponent<Animation>().Play("CameraXCutSceneBack");
            emissionPanel.SetActive(false);
            Debug.Log("EmissionOff");
            shadowPanel.SetActive(false);
            player.GetComponent<Animation>().Play("GhostCutSceneOff");
            phone.GetComponent<Animation>().Play("phoneback");
            phoneRenderer.enabled = true;
            myCamera.fieldOfView = 70f;
            PostPro.SetActive(false);
            recordingUI.SetActive(false);
            yield return new WaitForSeconds(0.01f);
            menu.cameraOn = false;
            phoneCollider.enabled = true;
            player.GetComponent<FirstPersonController>().enabled = true;
            crosshair.enabled = true;
            CutSceneDone = true;
            enemy.GetComponent<SkinnedMeshRenderer>().enabled = false;
            menu.objects.SetActive(false);
            ghostAI.SetActive(true);
            blinking = true;
            emissionPanel.SetActive(false);
            enemySkin.SetActive(false);
            closedPanel.SetActive(true);
        }
    }
}