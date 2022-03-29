using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{

    public class stove : MonoBehaviour
    {
        public GameObject container;
        public GameObject ice;
        public GameObject screwdriver;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(container.transform.childCount > 0)
            {
                screwdriver.transform.SetParent(container.transform);
                Destroy(ice);
            }
        }
    }

}
