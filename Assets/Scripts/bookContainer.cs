using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class bookContainer : MonoBehaviour
    {
        public GameObject book;
        public bookChecker bookCheck;
        public bool rightBook = false;
        public Raycast raycast;

        void Start()
        {
            bookCheck.container.Add(this.gameObject);
        }

        void Update()
        {
            if(transform.childCount == 2)
            {
                
                if(transform.GetChild(1).gameObject.tag == "book")
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                
            }
            else
            {
                this.GetComponent<BoxCollider>().enabled = true;
            }
        }

        public void Check()
        {
            if(transform.childCount > 0)
            {
                if(transform.GetChild(1).gameObject == book)
                {
                    rightBook = true;
                    bookCheck.count++;
                }
                
            }
        }
            
            
    }
}

