using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace scripts
{
    public class movementAI : MonoBehaviour
    {
        public NavMeshAgent enemy;
        public GameObject player;
        public GameObject collider;

        float timer = 0.0f;
        public float waitTime = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
            enemy = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            enemy.SetDestination(player.transform.position);
            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                if (collider.transform.localScale != new Vector3(0f, 1f, 0f))
                {
                    collider.transform.localScale = new Vector3(collider.transform.localScale.x - 1, collider.transform.localScale.y, collider.transform.localScale.z - 1);
                }
                timer = timer - waitTime;
                
            }        
        }
    }
}

