﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


namespace Scripts
{
    public class Raycast : MonoBehaviour
    {
        [SerializeField] private float raylength = 2.5f;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string excludeLayerName = null;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
        [SerializeField] public Image crosshair = null;
        private bool doOnce;
        public doorController door;
        public drawersAnim drawer;
        private const string interactableTag = "Interactable";
        private const string keyTag = "key";
        private const string doorTag = "Door";
        private const string lockedDoorTag = "LockedDoor";
        public bool isCrosshairActive;
        public PickUp pickup;
        public Transform crosshairpos;
        public Camera cam;
        public TMP_Text picktxt;
        public TMP_Text interact;
        public TMP_Text phoneText;
        public Animator anim;
        public Transform bookPos;
        public bookChecker check;
        public bool hasKey = false;
        public bool hasScrewdriver = false;
        Transform hours;
        public clock pendClock;
        public Inventory inventory;
        public holding held;
        public Transform stovePos;
        public Rotatelock lockScript;
        public GameObject player;
        public GameObject lockCam;
        public GameObject note;
        public GameObject objectives;
        public GameObject lockCamPP;
        public GameObject inventoryText;
        public InventoryUI inventoryUI;
        public bool inLockView = false;
        public bool inNoteView = false;
        public AudioSource drawerOpen;
        public bool invDoOnce = true;
        public TMP_Text blockedtxt;
        public GameObject containerBook;
        public bool storageDoor;
        public TMP_Text blockedDoortxt;



        void Start()
        {
            picktxt.gameObject.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;

        }

        public void noteAnim()
        {
            inNoteView = true;
            crosshair.enabled = false;
            interact.enabled = false;
            picktxt.enabled = false;
            note.SetActive(true);
            player.SetActive(false);
            inventoryUI.inventoryUI.SetActive(false);

        }
        // Update is called once per frame
        void Update()
        {

            if(bookPos != null)
            {
                bookPos.GetChild(0).gameObject.SetActive(false);
            }
            RaycastHit hit;
            RaycastHit bHit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            
            int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;


            if (Physics.Raycast(transform.position, fwd, out bHit, Mathf.Infinity, mask))
            {
                Debug.DrawRay(transform.position, fwd * bHit.distance, Color.red);
                if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(interactableTag))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Pick();
                        isCrosshairActive = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(doorTag))
                {
                    CrosshairChange(true);
                    door = hit.collider.gameObject.GetComponent<doorController>();
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    if(door.locked)
                    {
                        if(door.name == "storageDoor" && Input.GetKeyDown(KeyCode.E))
                        {
                            blockedtxt.gameObject.SetActive(true);
                            StartCoroutine(TextOffAfterTime());
                            
                        }
                        if (door.isWhiteDoor)
                        {
                            if(door.key.GetComponent<PickUp>().equipped)
                            {
                                if(Input.GetKeyDown(KeyCode.E) && hasKey)
                                {
                                    door.locked = false;
                                    door.audio.PlayOneShot(door.doorOpeningSFX);
                                    inventory.Remove(door.key.GetComponent<PickUp>().item);
                                    held.Remove(door.key);
                                    hasKey = false;
                                    Destroy(door.key);
                                }
                            }
                        }
                        if (door.isRedDoor)
                        {
                            if(door.key.GetComponent<PickUp>().equipped)
                            {
                                if(Input.GetKeyDown(KeyCode.E) && hasKey)
                                {
                                    door.locked = false;
                                    door.audio.PlayOneShot(door.doorOpeningSFX);
                                    inventory.Remove(door.key.GetComponent<PickUp>().item);
                                    held.Remove(door.key);
                                    hasKey = false;
                                    Destroy(door.key);
                                    SceneManager.LoadScene(0);
                                }
                            }
                        }
                    }

                    

                    if(Input.GetKeyDown(KeyCode.E) && door.locked)
                        {
                            door.audio.PlayOneShot(door.doorLockedSFX);
                            blockedDoortxt.gameObject.SetActive(true);
                            StartCoroutine(TextOffAfterTime());
                        }
                    
                    else if (Input.GetKeyDown(KeyCode.E) && door.locked == false && !door.doOnce)
                    {
                        door.PlayAnimation();
                        isCrosshairActive = true;
                    }

                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(keyTag))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);
                    interact.gameObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        hasKey = true;
                        isCrosshairActive = true;
                    }

                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("book"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);
                    interact.gameObject.SetActive(false);
                    var _parent = hit.collider.gameObject.transform.parent;
                    if(_parent != null)
                    {
                        containerBook = hit.collider.gameObject.transform.parent.gameObject;
                    }
                    

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                       
                        isCrosshairActive = true;
                        if(_parent != null && containerBook.GetComponent<bookContainer>().rightBook == true)
                        {
                            containerBook.GetComponent<bookContainer>().rightBook = false;
                            containerBook.GetComponent<bookContainer>().bookCheck.count--;
                        }

                        if(invDoOnce)
                        {
                            inventoryText.gameObject.SetActive(true);
                            invDoOnce = false;
                        }

                    }

                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("phone"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);
                    interact.gameObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        phoneText.gameObject.SetActive(true);
                        objectives.gameObject.SetActive(true);
                        pickup.Pick();
                        hit.collider.gameObject.layer = 7;
                        isCrosshairActive = true;
                        
                        
                        //doOnce = true;
                        
                    }

                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("container"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    bookPos = hit.collider.transform;
                    
                    
                    foreach(GameObject child in held.children)
                    {
                        if(child.activeSelf && (child.name == "redBook" || child.name == "lBlueBook" || child.name == "blueBook" || child.name == "greenBook" || child.name == "pinkBook" || child.name == "orangeBook"))
                        {
                            bookPos.GetChild(0).gameObject.SetActive(true);

                            if(Input.GetKeyDown(KeyCode.E) && child.activeSelf && (child.name == "redBook" || child.name == "lBlueBook" || child.name == "blueBook" || child.name == "greenBook" || child.name == "pinkBook" || child.name == "orangeBook"))
                            {
                                PickUp bookScript = child.GetComponent<PickUp>();
                                child.transform.SetParent(bookPos);
                                child.transform.localPosition = Vector3.zero;
                                child.transform.localRotation = Quaternion.Euler(Vector3.zero);
                                child.transform.localScale = Vector3.one;
                                inventory.Remove(bookScript.item);
                                held.Remove(child);
                                
                                bookPos.GetComponent<bookContainer>().Check();
                                
                            }
                        }
                        
                    }
                    
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("drawers"))
                {
                    CrosshairChange(true);
                    drawer = hit.collider.gameObject.GetComponent<drawersAnim>();
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E) && !drawer.doOnce)
                    {
                        
                        drawer.PlayAnimation();
                        isCrosshairActive = true;
                        drawerOpen.Play();

                        //doOnce = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("hours"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.hours();
                    }
                }
                 else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("minutes"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.minutes();
                    }
                }
                else if(Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("stoveContainer"))
                {
                    stovePos = hit.collider.transform;
                    foreach(GameObject child in held.children)
                    {
                        if(Input.GetKeyDown(KeyCode.E) && child.activeSelf && child.name == "Ice")
                        {
                            PickUp iceScript = child.GetComponent<PickUp>();
                            child.transform.SetParent(stovePos);
                            child.transform.localPosition = Vector3.zero;
                            child.transform.localRotation = Quaternion.Euler(Vector3.zero);
                            child.transform.localScale = Vector3.one;
                            inventory.Remove(iceScript.item);
                            held.Remove(child);

                        }
                    }
                }

                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("ice"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);
                    interact.gameObject.SetActive(false);


                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        pickup.transform.localPosition = new Vector3 (-1.34f, 0, 0);
                        isCrosshairActive = true;
                        //doOnce = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("screwdriver"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);
                    interact.gameObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log(pickup.item.name);
                        pickup.Pick();
                        pickup.transform.localPosition = new Vector3 (-0.5634038f, 0.295f, -1.037854f);
                        pickup.transform.localRotation = Quaternion.Euler(0, -75, 0);
                        isCrosshairActive = true;
                        hasScrewdriver = true;
                        //doOnce = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("lock"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    lockScript = hit.collider.gameObject.GetComponent<Rotatelock>();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inLockView = true;
                        crosshair.enabled = false;
                        interact.enabled = false;
                        lockCam.SetActive(true);
                        lockCamPP.SetActive(true);
                        player.SetActive(false);
                        inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
                    }
                }

                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("note"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    //noteScript = hit.collider.gameObject.GetComponent<NoteScript>();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        noteAnim();

                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("vent"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    var vent = hit.collider.gameObject;

                    if (Input.GetKeyDown(KeyCode.E) && hasScrewdriver)
                    {
                        vent.GetComponent<Animator>().Play("ventScrew");
                        GameObject.Find("ventEnterCollider").GetComponent<BoxCollider>().enabled = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("ventEnterCollider"))
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    var ventCol = hit.collider.gameObject;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EnterVent());
                    }
                    
                }
                else
                {
                    interact.gameObject.SetActive(false);
                    picktxt.gameObject.SetActive(false);
                    CrosshairChange(false);
                    //doOnce = false;
                    bookPos.GetChild(0).gameObject.SetActive(false);
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

        IEnumerator EnterVent()
        {
            if(player.transform.position.z > -16f)
            {
                player.GetComponent<Animation>().Play("enteringVent");
                yield return new WaitForSeconds(1.5f);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -16f);

            }
            else if (player.transform.position.z < -16f)
            {
                player.GetComponent<Animation>().Play("enteringVent");
                yield return new WaitForSeconds(1.5f);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -15f);
                
            }
        }
        
        IEnumerator TextOffAfterTime()
        {
            yield return new WaitForSeconds(1.5f);
            blockedtxt.gameObject.SetActive(false); 
            blockedDoortxt.gameObject.SetActive(false); 
        }
    }
}
