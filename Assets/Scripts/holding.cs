using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class holding : MonoBehaviour
    {
       public List<GameObject> children = new List<GameObject>();

       void Start()
       {

       }

       void Update()
       {
           foreach (GameObject child in children)
           {
               //Debug.Log(child);
           }
       }

       public void Remove(GameObject child)
       {
           children.Remove(child);
       }
    }
}

