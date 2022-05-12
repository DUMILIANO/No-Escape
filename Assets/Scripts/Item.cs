using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class Item : ScriptableObject 
    {
        new public string name = "Item";
        public Sprite icon = null;
        public bool important = false;
        public holding held;
        public Raycast raycast;

        public void Start()
        {
            held = GameObject.Find("rightHand").GetComponent<holding>();
            raycast = GameObject.Find("FirstPersonCharacter").GetComponent<Raycast>();
            
        }
        public void Use()
        {
            raycast = GameObject.Find("FirstPersonCharacter").GetComponent<Raycast>();
            foreach (GameObject child in held.children)
            {
                if(child.GetComponent<PickUp>().item.name == name)
                {
                        Debug.Log("pressed");
                        child.SetActive(true);
                        child.GetComponent<PickUp>().equipped = true;
                        if(child.tag == "key")
                        {
                            raycast.hasKey = true;
                        }
                        else if(child.tag == "screwdriver")
                        {
                            raycast.hasScrewdriver = true;
                        }
                        else
                        {
                            raycast.hasKey = false;
                            raycast.hasScrewdriver = false;
                        }

                    
                    
                }
                
                else
                {
                    child.SetActive(false);
                    child.GetComponent<PickUp>().equipped = false;
                }
                
            }
        }
    }

}
