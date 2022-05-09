using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class movementAI : MonoBehaviour
    {
        public float viewRadius = 15;
        public LayerMask playerMask;
        public float viewAngle = 90;
        public LayerMask obstacleMask;
        public NavMeshAgent enemy;
        public GameObject player;
        public GameObject collider;
        public ThirdPersonCharacter character;

        public AudioSource audio; 
        public AudioClip clip;
        [SerializeField]AudioClip[] audioClip;

        float timer = 0.0f;
        public float waitTime = 2.0f;
        public bool hittable = false;
        public AudioClip scream;
        public InventoryUI inventoryUI;

        public Transform[] points;
        private int destPoint = 0;

        bool playerInRange;
        Vector3 PlayerPosition = Vector3.zero;
        
        // Start is called before the first frame update
        void Start()
        {
            enemy = GetComponent<NavMeshAgent>();
            enemy.updateRotation = false;
            enemy.autoBraking = false;
            GoPoint();

        }

        void GoPoint()
        {
            enemy.destination = points[destPoint].position;
            destPoint = Random.Range(0, points.Length - 1);
        }
        void AIview()
        {
            Collider[] playerInRange_ = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
            for (int i = 0; i < playerInRange_.Length; i++)
            {
                Transform player_ = playerInRange_[i].transform;
                Vector3 dirToPlayer = (player_.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                    float dstToPlayer = Vector3.Distance(transform.position, player_.position);
                    if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                    {
                        playerInRange = true;
                        
                    }
                    else
                    {
                        playerInRange = false;
                    }
                }
                if (Vector3.Distance(transform.position, player_.position) > viewRadius)
                {
                    playerInRange = false;                
                }
                if (playerInRange)
                {
                    PlayerPosition = player_.transform.position;
                }
            }
        }
        private void Step()
        {
            AudioClip sfx = GetRandomClip();
            audio.PlayOneShot(sfx);
        }
        
        private AudioClip GetRandomClip()
        {
            int index = Random.Range(0, audioClip.Length - 1);
            return audioClip[index];
        }

        // Update is called once per frame
        void Update()
        {
            AIview();
            if(playerInRange)
            {
                enemy.SetDestination(player.transform.position);
                enemy.speed = 3f;
            }
            else
            {
                enemy.speed = 1f;
            }
            if(!enemy.pathPending && enemy.remainingDistance < 0.5f)
            {
                GoPoint();
            }
            
            timer += Time.deltaTime;
            if(timer > waitTime)
            {
                if (collider.transform.localScale != new Vector3(0f, 1f, 0f))
                {
                    collider.transform.localScale = new Vector3(collider.transform.localScale.x - 0.25f, collider.transform.localScale.y, collider.transform.localScale.z - 0.25f);
                }
                timer = timer - waitTime;
                
            }
            if(enemy.remainingDistance > enemy.stoppingDistance)
            {
                character.Move(enemy.desiredVelocity, false, false);
                //audio.PlayOneShot(clip);
                //audio.Play();
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }

            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if(Physics.Raycast(transform.position, fwd, out hit, 3.5f) && hit.collider.CompareTag("Door"))
            {
                Debug.Log("door");
                var door = hit.collider.gameObject.GetComponent<doorController>();
                
                if(door.locked == false  && !door.doOnce && !door.open)
                {
                    door.PlayAnimation();
                }
                if(door.locked)
                {
                    GoPoint();
                }
            }
                    
        }
        void OnCollisionEnter(Collision collision) 
        {
            Debug.Log("collision");
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("Dead");
                StartCoroutine(deathScene());
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "collider")
            {
                hittable = true;
                Debug.Log("Heartbeat");
                audio.Play();
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if(other.tag == "collider")
            {
                hittable = false;
                Debug.Log("Heartbeat");
                audio.Pause();
            }
        }
        IEnumerator deathScene()
        {
            audio.PlayOneShot(scream);
            yield return new WaitForSeconds(3.5f);
            SceneManager.LoadScene(0);
            inventoryUI.cursorIsLocked = !inventoryUI.cursorIsLocked;
        }
    } 
}

