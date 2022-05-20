using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts
{
    public class Doll : MonoBehaviour
    {

        public GameObject doll;
        public GameObject dollContainer;
        public bool doOnce;
        public bool dollPlaced;

        void Update()
        {
            if(dollContainer.transform.childCount > 0 && !doOnce)
            {
                doll.gameObject.layer = 0;
                doll.transform.SetParent(dollContainer.transform);
                doll.transform.localScale = new Vector3 ( 0.4f, 0.4f, 0.4f);
                doll.GetComponent<BoxCollider>().enabled = false;
                doOnce = true;
                dollPlaced = true;
            }


        }
    }



}

