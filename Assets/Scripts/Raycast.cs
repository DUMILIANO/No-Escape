using System.Collections;
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
        public drawersAnim drawer;
        private const string interactableTag = "PickUp";
        private const string keyTag = "key";
        private const string doorTag = "Door";
        private const string lockedDoorTag = "LockedDoor";
        public bool isCrosshairActive;
        public PickUp pickup;
        public Transform crosshairpos;
        public Rotatelock lockScript;
        public Camera cam;
        public Text picktxt;
        public Animator anim;
        public Transform bookPos;
        public bookChecker check;
        public bool hasKey = false;
        Transform hours;
        public GameObject player;
        public GameObject lockCam;
        public clock pendClock;
        public Inventory inventory;
        public holding held;
        public InventoryUI inventoryUI;

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
                    if(door.locked)
                    {
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
                                }
                            }
                        }   
                        
                    }
                    if(Input.GetKeyDown(KeyCode.E) && door.locked)
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
                else if (hit.collider.CompareTag("phone") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        hit.collider.gameObject.layer = 7;
                        isCrosshairActive = true;
                        Debug.Log(hit.collider.transform.parent);
                        if(hit.collider.transform.parent != null && hit.collider.transform.parent.GetComponent<bookContainer>().rightBook)
                        {
                            hit.collider.transform.parent.GetComponent<bookContainer>().rightBook = false;
                            check.count--;
                        }
                        
                        //doOnce = true;
                        
                    }

                }
                else if (hit.collider.CompareTag("container") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    bookPos = hit.collider.transform;
                    foreach(GameObject child in held.children)
                    {
                        if(Input.GetKeyDown(KeyCode.E) && child.activeSelf && (child.name == "book1" || child.name == "book2" || child.name == "book3" || child.name == "book4" || child.name == "book5" || child.name == "book6"))
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
                else if (hit.collider.CompareTag("drawers") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    drawer = hit.collider.gameObject.GetComponent<drawersAnim>();
                    picktxt.gameObject.SetActive(true);
                   
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        drawer.PlayAnimation();
                        isCrosshairActive = true;
                        //doOnce = true;
                    }
                }
                else if (hit.collider.CompareTag("hours") && Physics.SphereCast(transform.position, 10f, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.hours();
                    }
                }
                else if (hit.collider.CompareTag("minutes") && Physics.SphereCast(transform.position, 10f, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.minutes();
                    }
                }

                else if (hit.collider.CompareTag("lock") && Physics.Raycast(transform.position, fwd, out hit, raylength, mask))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    lockScript = hit.collider.gameObject.GetComponent<Rotatelock>();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        crosshair.enabled = false;
                        picktxt.enabled = false;
                        lockCam.SetActive(true);
                        player.SetActive(false);
                        inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
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
