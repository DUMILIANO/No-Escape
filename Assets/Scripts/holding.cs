using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
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
    }
}

