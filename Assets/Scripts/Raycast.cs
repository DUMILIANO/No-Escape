using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace scripts
{
    public class raycast : MonoBehaviour
    {
        [SerializeField] private float raylength = 2.5f;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string excludeLayerName = null;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
        [SerializeField] private Image crosshair = null;
        private bool doOnce;
        private int num;
        public doorController door;
        private const string interactableTag = "PickUp";
        private const string doorTag = "Door";
        public bool isCrosshairActive;
        public GameObject tape;
        public PickUp pickup;
        public Transform crosshairpos;
        public Camera cam;
        public Text picktxt;
        public AudioClip[] rec;
        public AudioSource audiosource;
        public Animator anim;


        void Start()
        {
            picktxt.gameObject.SetActive(false);
            num = -1;
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity, mask))
            {

                Debug.DrawRay(transform.position, fwd * hit.distance, Color.red);
                crosshairpos.position = cam.WorldToScreenPoint(hit.point);
                if (hit.collider.CompareTag(interactableTag) && PickUp.slotFull == false && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    tape = hit.collider.gameObject;
                    anim = hit.collider.gameObject.GetComponent<Animator>();
                    picktxt.gameObject.SetActive(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Pick();
                        isCrosshairActive = true;
                        //doOnce = true;
                        pickup.pickable = false;
                    }
                }
                else if (hit.collider.CompareTag(doorTag) && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    door = hit.collider.gameObject.GetComponent<doorController>();
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        door.PlayAnimation();
                        isCrosshairActive = true;
                        //doOnce = true;
                    }

                }
                else if (hit.collider.CompareTag("recorder") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && PickUp.slotFull)
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(tape);
                        num = num + 1;
                        audiosource = hit.collider.gameObject.GetComponent<AudioSource>();
                        CrosshairChange(true);
                        PickUp.slotFull = false;
                        pickup.Drop();
                        audiosource.PlayOneShot(rec[num]);
                        
                    }
                    
                }

                else
                {
                    picktxt.gameObject.SetActive(false);
                    CrosshairChange(false);
                    //doOnce = false;
                }
            }
            if(num >= 3)
            {
                StartCoroutine(end());
                
            }

            if(pickup.equipped && PickUp.slotFull)
            {
                anim.Play("line", 0, 0.0f);
            }


        }
        void CrosshairChange(bool on)
        {
            if (on)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
        IEnumerator end()
        {
            yield return new WaitForSeconds(16);
            SceneManager.LoadScene(3);
        }
    }

}
