using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class PickUp : MonoBehaviour
    {
        public Item item;
        public Rigidbody rb;
        public BoxCollider coll;
        public Transform player, container, cam;

        public bool equipped;
        public static bool slotFull;
        public bool pickable;
        public bool dropable;
        public holding held;

        void SetLayerRecursively(GameObject obj, int newLayer)
            {
                obj.layer = newLayer;
            
                foreach(Transform child in obj.transform )
                {
                    SetLayerRecursively( child.gameObject, newLayer );
                }
            }
        // Start is called before the first frame update
        void Start()
        {
            if (!equipped)
            {
                rb.isKinematic = false;
                //coll.isTrigger = false;
                pickable = true;
                dropable = false;
            }
            if (equipped)
            {
                rb.isKinematic = true;
                coll.isTrigger = true;
                slotFull = true;
                pickable = false;
                dropable = true;
            }
            item.held = container.GetComponent<holding>();
        }


        // Update is called once per frame
        void Update()
        {
            //Vector3 distanceToPlayer = player.position - transform.position;
            //if (!equipped && !slotFull) pickable = true;

            //if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
            //Debug.Log(pickable);

        }

        public void Pick()
        {
            if(item.important)
            {
                bool wasPickedUp = Inventory.instance.Add(item);
                if (wasPickedUp)
                {
                    this.gameObject.GetComponent<AudioSource>().Play();
                    SetLayerRecursively(this.gameObject, 7);
                    Debug.Log(this.gameObject);
                    equipped = true;
                    slotFull = true;

                    transform.SetParent(container);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);
                    transform.localScale = Vector3.one;

                    rb.isKinematic = true;
                    coll.isTrigger = true;

                    pickable = false;
                    dropable = true;
                    held.children.Add(gameObject);
                    if(held.children.Count > 1)
                    {
                        held.children[held.children.Count - 2].GetComponent<PickUp>().equipped = false;
                        held.children[held.children.Count - 2].SetActive(false);
                        
                    }
                    if(item.name != "tool" || item.name != "Key")
                    {
                        var raycast = GameObject.Find("FirstPersonCharacter").GetComponent<Raycast>();
                        raycast.hasKey = false;
                        raycast.hasScrewdriver = false;
                    }
                    
                }
            }
            
            else
            {
                equipped = true;
                slotFull = true;

                transform.SetParent(container);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                transform.localScale = Vector3.one;

                rb.isKinematic = true;
                coll.isTrigger = true;

                pickable = false;
                dropable = true;
            } 
            

            
            

        }
        public void Drop()
        {
            if(!item.important)
            {
                equipped = false;
                slotFull = false;

                transform.SetParent(null);
                rb.isKinematic = false;
                coll.isTrigger = false;
                pickable = true;
                dropable = false;
            }
            
        }
    }
}