using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class bookChecker : MonoBehaviour
    {
        public doorController door;
        public Raycast raycast;
        public List<GameObject> container = new List<GameObject>();
        public int count;
        void Update()
        {
            foreach (GameObject book in container)
            {
                if(book.GetComponent<bookContainer>().rightBook == true && count == 6)
                {
                    door.locked = false;
                }
            }
        }
    }
}