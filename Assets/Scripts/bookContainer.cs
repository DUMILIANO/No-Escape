using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class bookContainer : MonoBehaviour
    {
        public GameObject book;
        public bookChecker bookCheck;
        public bool rightBook = false;

        void Start()
        {
            bookCheck.container.Add(this.gameObject);
        }

        void Update()
        {

        }

        public void Check()
        {
            if(transform.childCount > 0)
            {
                if(transform.GetChild(0).gameObject == book)
                {
                    rightBook = true;
                    bookCheck.count++;
                }
                
            }
        }
            
            
    }
}
