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
        private const string interactableTag = "Interactable";
        private const string keyTag = "key";
        private const string doorTag = "Door";
        private const string lockedDoorTag = "LockedDoor";
        public bool isCrosshairActive;
        public PickUp pickup;
        public Transform crosshairpos;
        public Camera cam;
        public Text picktxt;
        public Animator anim;
        public Transform bookPos;
        public bookChecker check;
        public bool hasKey = false;
        Transform hours;
        public clock pendClock;
        public Inventory inventory;
        public holding held;
        public Transform stovePos;

        void Start()
        {
            picktxt.gameObject.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;

        }

        // Update is called once per frame
        void Update()
        {
            

            RaycastHit hit;
            RaycastHit bHit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            
            int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

            

            if (Physics.Raycast(transform.position, fwd, out bHit, Mathf.Infinity, mask))
            {
                //shows a ray whenever it hits with something
                Debug.DrawRay(transform.position, fwd * bHit.distance, Color.red);
                //checks if the ray is interactable and makes sures the player isn't already holding something
                if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(interactableTag))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    if (Input.GetKey(KeyCode.E))
                    {
                        pickup.Pick();
                        isCrosshairActive = true;
                        //doOnce = true;
                        /*Debug.Log(hit.collider.transform.parent);
                        if(hit.collider.transform.parent != null && hit.collider.transform.parent.GetComponent<bookContainer>().rightBook)
                        {
                            hit.collider.transform.parent.GetComponent<bookContainer>().rightBook = false;
                            check.count--;
                        }*/
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(doorTag))
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag(keyTag))
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("book"))
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("phone"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        hit.collider.gameObject.layer = 7;
                        isCrosshairActive = true;
                        
                        
                        //doOnce = true;
                        
                    }

                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("container"))
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("drawers"))
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("hours"))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.hours();
                    }
                }
                 else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("minutes"))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
                    hours = hit.collider.transform;
                    
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pendClock.minutes();
                    }
                }
                else if(Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("stoveContainer"))
                {
                    CrosshairChange(true);
                    picktxt.gameObject.SetActive(true);
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

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        isCrosshairActive = true;
                        //doOnce = true;
                    }
                }
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("screwdriver"))
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
