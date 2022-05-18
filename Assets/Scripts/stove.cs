using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class stove : MonoBehaviour
    {
        public GameObject container;
        public GameObject ice;
        public GameObject iceCube;
        public GameObject screwdriver;
        public BoxCollider screwdriverCollider;
        public GameObject puddle;
        public bool doOnce;
        public Inventory inventory;
        public holding held;
        public ParticleSystem smoke;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(container.transform.childCount > 0 && !doOnce)
            {
                screwdriver.transform.SetParent(container.transform);
                iceCube.GetComponent<Animation>().Play("MeltingIce");
                ice.gameObject.tag = "Untagged";
                smoke.Play();
                screwdriver.GetComponent<Animation>().Play("screwdriver");
                puddle.GetComponent<Animation>().Play("puddle");
                StartCoroutine(DestroyIce());
                doOnce = true;
                
            }
        }

        IEnumerator DestroyIce()
        {
            //Activates ice break shader.
            yield return new WaitForSeconds(7);
            inventory.Remove(ice.GetComponent<PickUp>().item);
            held.Remove(ice);  
            Destroy(ice);
            screwdriver.gameObject.layer = 0;
            ice = null;
            iceCube = null;
            screwdriverCollider.enabled = true;
            /*if(screwdriver.GetComponent<Rigidbody>() == null)
            {
                screwdriver.AddComponent<Rigidbody>();
            }
            
            screwdriver.GetComponent<PickUp>().rb = screwdriver.GetComponent<Rigidbody>();*/
        }
    }

}
