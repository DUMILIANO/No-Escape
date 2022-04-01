using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{

    public class stove : MonoBehaviour
    {
        public GameObject container;
        public GameObject ice;
        public GameObject iceCube;
        public GameObject screwdriver;
        public BoxCollider screwdriverCollider;
        
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
                iceCube.GetComponent<Animation>().Play("MeltingIce");
                screwdriver.GetComponent<Animation>().Play("screwdriver");
                StartCoroutine(DestroyIce());
            }
        }

        IEnumerator DestroyIce()
        {
            //Activates ice break shader.
            yield return new WaitForSeconds(7);       
            Destroy(ice);
            ice = null;
            iceCube = null;
            screwdriverCollider.enabled = true;
            screwdriver.AddComponent<Rigidbody>();
            screwdriver.GetComponent<PickUp>().rb = screwdriver.GetComponent<Rigidbody>();



        }
    }

}
