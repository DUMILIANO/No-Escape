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
        public FirstPersonController firstPersonController;
        public Image crosshair;
        public Animation phoneAnim;
        public bookChecker puzzleDone;


        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && animPlayed == false && puzzleDone.puzzleComplete == true)
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                crosshair.enabled = false;
                gCutscene.Play();
                CameraShaker.Instance.ShakeOnce(1f, 0.86f, 0.10f, 2f);
                animPlayed = true;
                phoneAnim.Play();



            }
        }
    }
}