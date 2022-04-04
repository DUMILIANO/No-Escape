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
        // Start is called before the first frame update
        void Start()
        {
            enemy = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            enemy.SetDestination(player.transform.position);
            if(collider.transform.localScale != new Vector3(0f, 1f, 0f))
            {
                StartCoroutine(shrinkCollider());
            }
                
        }

        IEnumerator shrinkCollider()
        {
            collider.transform.localScale = new Vector3(collider.transform.localScale.x - 1, collider.transform.localScale.y, collider.transform.localScale.z - 1);
            yield return new WaitForSeconds(1f);
        }
    }
}

