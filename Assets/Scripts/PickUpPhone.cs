﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class PickUpPhone : MonoBehaviour
    {
        public Rigidbody rb;
        public BoxCollider coll;
        public Transform player, container, cam;

        public bool equipped;
        public static bool slotFull;
        public bool pickable;
        public bool dropable;
        public GameObject phoneLayer;

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
                phoneLayer.layer = LayerMask.NameToLayer("ItemInFront");
            }
        }


        // Update is called once per frame
        void Update()
        {
            Vector3 distanceToPlayer = player.position - transform.position;
            //if (!equipped && !slotFull) pickable = true;

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
    }
}