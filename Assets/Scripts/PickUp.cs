using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
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
        public Raycast raycastScript;
        public holding held;

        // Start is called before the first frame update
        void Start()
        {
            if (!equipped)
            {
                rb.isKinematic = false;
                coll.isTrigger = false;
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
            Vector3 distanceToPlayer = player.position - transform.position;
            //if (!equipped && !slotFull) pickable = true;

            if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
            //Debug.Log(pickable);

        }

        public void Pick()
        { 
            bool wasPickedUp = Inventory.instance.Add(item);

            if(wasPickedUp)
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
                held.children.Add(gameObject);
                if(held.children.Count > 1)
                {
                    held.children[held.children.Count - 2].SetActive(false);
                }
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
                raycastScript.hasKey = false;
            }
            
        }
    }
}