using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Scripts
{
    public class GameMaster : MonoBehaviour
    {
        private static GameMaster instance;
        public Vector3 lastCheckPointPos;


        // Start is called before the first frame update
        void Awake()
        {
            GameObject.FindWithTag("Player").transform.position = lastCheckPointPos;
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);  
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
 
