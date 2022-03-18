using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class Item : ScriptableObject 
    {
        new public string name = "Item";
        public Sprite icon = null;
        public bool important = false;
        public holding held;

        public void Start()
        {
            held = GameObject.Find("container").GetComponent<holding>();
        }
        public void Use()
        {
            foreach (GameObject child in held.children)
            {
                if(child.GetComponent<PickUp>().item.name == name)
                {
                    child.SetActive(true);
                }
                else
                {
                    child.SetActive(false);
                }
            }
        }
    }

}
