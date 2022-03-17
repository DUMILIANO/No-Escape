using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class PickUp : MonoBehaviour
    {
        public Rigidbody rb;
        public BoxCollider coll;
        public Transform player, container, cam;

        public float pickUpRange;
        public float dropForwardForce, dropUpwardForce;

        public bool equipped;
        public static bool slotFull;
        public bool pickable;
        public bool dropable;
        public AudioClip recording;
        public Raycast raycastScript;

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
        public void Drop()
        {
            equipped = false;
            slotFull = false;

            transform.SetParent(null);

            rb.velocity = player.GetComponent<Rigidbody>().velocity;
            rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
            float random = Random.Range(-1f, 1f);
            rb.AddTorque(new Vector3(random, random, random) * 10);
            rb.isKinematic = false;
            coll.isTrigger = false;
            pickable = true;
            dropable = false;
            raycastScript.hasKey = false;
        }
    }
}