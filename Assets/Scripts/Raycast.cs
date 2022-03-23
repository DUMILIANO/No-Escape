﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace scripts
{
    public class Raycast : MonoBehaviour
    {
        [SerializeField] private float raylength = 2.5f;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string excludeLayerName = null;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
        [SerializeField] private Image crosshair = null;
        private bool doOnce;
        public doorController door;
        private const string interactableTag = "PickUp";
        private const string keyTag = "key";
        private const string doorTag = "Door";
        private const string lockedDoorTag = "LockedDoor";
        public bool isCrosshairActive;
        public PickUp pickup;
        public Transform crosshairpos;
        public Camera cam;
        public Text picktxt;
        public Animator anim;
        [SerializeField] public bool hasKey = false;

        public Inventory inventory;
        public holding held;

        void Start()
        {
            picktxt.gameObject.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;

        }

        // Update is called once per frame
        void Update()
        {
            

            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            
            int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

            

            if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity, mask))
            {
                //shows a ray whenever it hits with something
                Debug.DrawRay(transform.position, fwd * hit.distance, Color.red);
                //checks if the ray is interactable and makes sures the player isn't already holding something
                if (hit.collider.CompareTag(interactableTag) && PickUp.slotFull == false && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
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

                    if (door.locked == true  && GameObject.Find("key").GetComponent<PickUp>().equipped && door.isWhiteDoor && GameObject.Find("key").activeSelf)
                    {
                        if(Input.GetKeyDown(KeyCode.E) && hasKey)
                        {
                            door.locked = false;
                            door.audio.PlayOneShot(door.doorOpeningSFX);
                            inventory.Remove(GameObject.Find("key").GetComponent<PickUp>().item);
                            held.Remove(GameObject.Find("key"));
                            Destroy(GameObject.Find("key"));
                        }
                    }
                    else if(Input.GetKeyDown(KeyCode.E) && door.locked == true)
                    {
                        door.audio.PlayOneShot(door.doorLockedSFX);
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && door.locked == false)
                    {
                        door.PlayAnimation();
                        isCrosshairActive = true;
                        //doOnce = true;
                    }

                }
                /*else if (hit.collider.CompareTag(lockedDoorTag) && Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hasKey)
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

                }*/
                else if (hit.collider.CompareTag(keyTag) && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        hasKey = true;
                        isCrosshairActive = true;
                        //doOnce = true;
                    }

                }
                else if (hit.collider.CompareTag(interactableTag) && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        isCrosshairActive = true;
                        //doOnce = true;
                    }

                }
                else if (hit.collider.CompareTag("container") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);

                    if(Input.GetKeyDown(KeyCode.E) && GameObject.Find("book").activeSelf)
                    {
                        Debug.Log("Book placed");
                    }
                }

                else
                {
                    picktxt.gameObject.SetActive(false);
                    CrosshairChange(false);
                    //doOnce = false;
                }
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
    }

}
