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
        public string animationName;
        public Animator doorAnim;



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
            phone.GetComponent<Animation>().Play("phone");
            StartCoroutine(Fade());
            yield return new WaitForSeconds(0.9f);
            myCamera.fieldOfView = 30f;
            PostPro.SetActive(true);
            recordingUI.SetActive(true);
            phoneRenderer.enabled = false;
            phoneCollider.enabled = false;
            enemy.GetComponent<SkinnedMeshRenderer>().enabled = true;
            enemy.GetComponent<Animation>().Play("GhostMovingCutScene");
            yield return new WaitForSeconds(6f);
            enemy.GetComponent<Animation>().Play("Armature_ArmatureAction");
            yield return new WaitForSeconds(3f);
            StartCoroutine(PhoneOffAnimation());
        }

        IEnumerator PhoneOffAnimation()
        {
            StartCoroutine(Fade());
            yield return new WaitForSeconds(1f);
            emissionPanel.SetActive(false);
            shadowPanel.SetActive(false);
            FirstPersonController.GetComponent<Animation>().Play("CameraXCutSceneBack");
            player.GetComponent<Animation>().Play("GhostCutSceneOff");
            phone.GetComponent<Animation>().Play("phoneback");
            phoneRenderer.enabled = true;
            myCamera.fieldOfView = 70f;
            PostPro.SetActive(false);
            recordingUI.SetActive(false);
            yield return new WaitForSeconds(0.01f);
            phoneCollider.enabled = true;
            enemy.GetComponent<SkinnedMeshRenderer>().enabled = false;
            player.GetComponent<FirstPersonController>().enabled = true;
            crosshair.enabled = true;
            doorAnim.Play(animationName, 0, 0.0f);

        }
    }
}