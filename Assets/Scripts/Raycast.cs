using System.Collections;
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
        public Transform dollPos;
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
        public TMP_Text noteTxt;
        public GameObject lockObject;
        public TMP_Text leaveLockTxt;
        public TMP_Text leaveNotetxt;
        public GameObject screwdriver;
        public TMP_Text bookTxt;
        public CutScene finishedCutScene;
        public GameObject painting;
        public GameObject propLock;
        public TMP_Text ventTxt;
        public TMP_Text iceTxt;
        public TMP_Text newspaper;
        public bool inBasement = false;
        public Transform target;
        public Transform tpTarget;
        public GameObject thePlayer;
        public Rigidbody body;
        public Doll badEnding;
        public GameObject panel;
        public GameObject leftHand;
        public GameObject rightHand;
        public GameObject panelBadEnding;
        public GameObject roomPainting;
        public bool paintingDoOnce;
        public GameObject portraitPanel;
        public bool portraitMoved = false;
        public LockControl isSolved;
        public bool hasDoll = false;
        public AudioSource deathAmbience;
        public AudioSource whisper;
        public bookChecker complete;
        public TMP_Text place;
        public TMP_Text gettingCloser;
        public TMP_Text exitDoor;
        public GameObject pausedMenu;
        

        void Start()
        {
            picktxt.gameObject.SetActive(false);
        }

        public void noteAnim()
        {
            inNoteView = true;
            crosshair.enabled = false;
            interact.enabled = false;
            picktxt.enabled = false;
            note.SetActive(true);
            noteTxt.gameObject.SetActive(true);
            player.SetActive(false);
            leaveNotetxt.gameObject.SetActive(true);
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
                        if (door.name == "exitDoor" && Input.GetKeyDown(KeyCode.E))
                        {
                            exitDoor.gameObject.SetActive(true);
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
                        if (door.name == "basementHouseDoor" && Input.GetKeyDown(KeyCode.E) && hasKey && door.key.GetComponent<PickUp>().equipped)
                        {
                            Debug.Log("WOadas");
                            StartCoroutine(EnterBasement());
                            Destroy(door.key);
                            door.audio.PlayOneShot(door.doorOpeningSFX);
                            inventory.Remove(door.key.GetComponent<PickUp>().item);
                            held.Remove(door.key);
                            hasKey = false;
                            door.locked = false;
                        }
                        if (door.isRedDoor && badEnding.dollPlaced == false)
                        {
                            if(door.key.GetComponent<PickUp>().equipped)
                            {
                                if(Input.GetKeyDown(KeyCode.E) && hasKey)
                                {
                                    Debug.Log("NO THIS ONE");
                                    door.locked = false;
                                    door.audio.PlayOneShot(door.doorOpeningSFX);
                                    inventory.Remove(door.key.GetComponent<PickUp>().item);
                                    held.Remove(door.key);
                                    hasKey = false;
                                    Destroy(door.key);
                                    player.GetComponent<FirstPersonController>().enabled = false;
                                    StartCoroutine(BadEnding());
                                    
                                }
                            }
                        }
                        else if (door.isRedDoor && badEnding.dollPlaced == true)
                        {
                            if (door.key.GetComponent<PickUp>().equipped)
                            {
                                if (Input.GetKeyDown(KeyCode.E) && hasKey)
                                {
                                    Debug.Log("THIS ONE");
                                    door.locked = false;
                                    door.audio.PlayOneShot(door.doorOpeningSFX);
                                    inventory.Remove(door.key.GetComponent<PickUp>().item);
                                    held.Remove(door.key);
                                    hasKey = false;
                                    Destroy(door.key);
                                    panel.gameObject.SetActive(true);
                                    StartCoroutine(GoodEnding());
                                     

                                }
                            }
                        }

                    }

                    

                    if (door.name == "basementDoor" && Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(EnterHouse());
                    }

                    if (Input.GetKeyDown(KeyCode.E) && door.locked && door.name != "exitDoor")
                    {
                        door.audio.PlayOneShot(door.doorLockedSFX);
                        if(door.name != "storageDoor")
                        {
                            blockedDoortxt.gameObject.SetActive(true);
                            StartCoroutine(TextOffAfterTime());
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && door.locked == false && door.name == "basementHouseDoor")
                    {
                        Debug.Log("working");
                        StartCoroutine(EnterBasement());
                        isCrosshairActive = true;
                    }                  
                    else if (Input.GetKeyDown(KeyCode.E) && door.locked == false && !door.doOnce)
                    {
                        Debug.Log("working");
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
                        StartCoroutine(TextOffAfterTime());
                        isCrosshairActive = true;
                        if(_parent != null)
                        {
                            containerBook.GetComponent<bookContainer>().bookCheck.allCount--;
                            if(containerBook.GetComponent<bookContainer>().rightBook == true)
                            {
                                containerBook.GetComponent<bookContainer>().rightBook = false;
                                containerBook.GetComponent<bookContainer>().bookCheck.count--;
                            }
                            
                        }

                        if(invDoOnce)
                        {
                            inventoryText.gameObject.SetActive(true);
                            bookTxt.gameObject.SetActive(true);
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
                    CrosshairChange(false);
                    interact.gameObject.SetActive(false);
                    picktxt.gameObject.SetActive(false);
                    bookPos = hit.collider.transform;
                                       
                    foreach(GameObject child in held.children)
                    {
                        if(child.activeSelf && (child.name == "redBook" || child.name == "lBlueBook" || child.name == "blueBook" || child.name == "greenBook" || child.name == "pinkBook" || child.name == "orangeBook"))
                        {
                            bookPos.GetChild(0).gameObject.SetActive(true);
                            place.gameObject.SetActive(true);

                            if (Input.GetKeyDown(KeyCode.Mouse0) && child.activeSelf && (child.name == "redBook" || child.name == "lBlueBook" || child.name == "blueBook" || child.name == "greenBook" || child.name == "pinkBook" || child.name == "orangeBook"))
                            {
                                child.gameObject.layer = 0;
                                child.transform.GetChild(0).gameObject.layer = 0;
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("hours") && complete.puzzleComplete == true)
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
                 else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("minutes") && complete.puzzleComplete == true)
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
                        if(Input.GetKeyDown(KeyCode.Mouse0) && child.activeSelf && child.name == "Ice")
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

                else if(Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("dollContainer") && hasDoll == true)
                {
                    CrosshairChange(true);
                    place.gameObject.SetActive(true);
                    dollPos = hit.collider.transform;
                    foreach(GameObject child in held.children)
                    {
                        if(Input.GetKeyDown(KeyCode.Mouse0) && child.activeSelf && child.name == "doll")
                        {
                            PickUp doll = child.GetComponent<PickUp>();
                            child.transform.SetParent(dollPos);
                            child.transform.localPosition = Vector3.zero;
                            child.transform.localRotation = Quaternion.Euler(Vector3.zero);
                            child.transform.localScale = Vector3.one;
                            inventory.Remove(doll.item);
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
                        iceTxt.gameObject.SetActive(true);
                        StartCoroutine(TextOffAfterTime());
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("lock") && isSolved.solved == false)
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);
                    lockScript = hit.collider.gameObject.GetComponent<Rotatelock>();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        leaveLockTxt.gameObject.SetActive(true);
                        propLock.SetActive(false);
                        lockObject.SetActive(true);                     
                        inLockView = true;
                        crosshair.enabled = false;
                        interact.enabled = false;
                        lockCam.SetActive(true);
                        lockCamPP.SetActive(true);
                        player.SetActive(false);
                        inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
                    }

                    if(isSolved.solved == true)
                    {
                        crosshair.enabled = true;
                        lockCam.SetActive(false);
                        player.SetActive(true);
                        inventoryUI.cursorIsLocked = true;
                        lockObject.SetActive(false);
                        leaveLockTxt.gameObject.SetActive(false);


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
                        vent.GetComponent<AudioSource>().Play();
                        GameObject.Find("ventEnterCollider").GetComponent<BoxCollider>().enabled = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && !hasScrewdriver)
                    {
                        ventTxt.gameObject.SetActive(true);
                        StartCoroutine(TextOffAfterTime());
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
                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("CorridorPainting") && finishedCutScene.CutSceneDone == true && portraitMoved == false)
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    picktxt.gameObject.SetActive(false);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        portraitMoved = true;
                        painting.GetComponent<Animation>().Play("paintingAnim");
                    }
                }

                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("SpawnroomPainting") && hasScrewdriver == true && paintingDoOnce == false)
                {
                    CrosshairChange(true);
                    interact.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && hasScrewdriver == true)
                    {
                        paintingDoOnce = true;
                        player.GetComponent<FirstPersonController>(). enabled = false;
                        portraitPanel.GetComponent<Animation>().Play("breakingPanel");
                        StartCoroutine(BreakPainting());                  
                    }

                }

                else if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask) && hit.collider.CompareTag("doll"))
                {
                    CrosshairChange(true);
                    pickup = hit.collider.gameObject.GetComponent<PickUp>();
                    interact.gameObject.SetActive(false);
                    picktxt.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickup.Pick();
                        hasDoll = true;
                        pickup.transform.localPosition = new Vector3 (-0.2f, 0f, 0f);
                        pickup.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    }
                }

                
                else
                {
                    interact.gameObject.SetActive(false);
                    picktxt.gameObject.SetActive(false);
                    place.gameObject.SetActive(false);
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

        IEnumerator BreakPainting()
        {
            yield return new WaitForSeconds(0.5f);
            roomPainting.GetComponent<Animation>().Play("portraitAnim");
            player.GetComponent<FirstPersonController>(). enabled = true;
        }

        IEnumerator EnterVent()
        {
            if(player.transform.position.z > -16f)
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                player.GetComponent<Animation>().Play("enteringVent");
                Debug.Log("entering1");
                yield return new WaitForSeconds(1f);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -16f);
                yield return new WaitForSeconds(0.1f);
                player.GetComponent<FirstPersonController>().enabled = true;

            }
            else if (player.transform.position.z < -15f)
            {
                player.GetComponent<FirstPersonController>().enabled = false;
                player.GetComponent<Animation>().Play("enteringVent");
                yield return new WaitForSeconds(1f);
                Debug.Log("entering2");
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -15f);
                yield return new WaitForSeconds(0.1f);
                player.GetComponent<FirstPersonController>().enabled = true;
            }
        }

        IEnumerator EnterBasement()
        {
            player.GetComponent<Animation>().Play("enteringBasement");
            yield return new WaitForSeconds(0.5f);
            player.transform.position = new Vector3 (5.44f, -0.53f, -8.7f);
            //player.transform.localRotation = Quaternion.Euler (0f, -160f, 0f);
            thePlayer.transform.rotation = tpTarget.transform.rotation;
            FindObjectOfType<FirstPersonController>().m_MouseLook.SetRotation(tpTarget.transform.rotation);
            inBasement = true;
        }

        IEnumerator EnterHouse()
        {
            player.GetComponent<Animation>().Play("enteringBasement");
            yield return new WaitForSeconds(0.5f);
            player.transform.position = new Vector3(25.84667f, -0.2427391f, -0.9764763f);
            inBasement = false;
        }


        IEnumerator TextOffAfterTime()
        {
            yield return new WaitForSeconds(2f);
            blockedtxt.gameObject.SetActive(false); 
            blockedDoortxt.gameObject.SetActive(false);
            bookTxt.gameObject.SetActive(false);
            ventTxt.gameObject.SetActive(false);
            iceTxt.gameObject.SetActive(false);
            exitDoor.gameObject.SetActive(false);
        }

        IEnumerator BadEnding()
        {
            whisper.Play();
            yield return new WaitForSeconds(0.5f);
            deathAmbience.Play();
            crosshair.enabled = false;
            interact.enabled = false;
            player.GetComponent<Animation>().Play("PlayerInPos");
            yield return new WaitForSeconds(0.5f);
            leftHand.GetComponent<Animation>().Play("leftHand");
            rightHand.GetComponent<Animation>().Play("rightHand");
            yield return new WaitForSeconds(0.3f);
            leftHand.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            rightHand.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
            player.GetComponent<Animation>().Play("BadEnding");
            yield return new WaitForSeconds(1f);
            panelBadEnding.GetComponent<Animation>().Play("BadEndingBlack");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(3);
            

        }

        IEnumerator GoodEnding()
        {
            yield return new WaitForSeconds(0.5f);
            panel.GetComponent<Animation>().Play("endingfade");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(3);
            
        }
    }
}
