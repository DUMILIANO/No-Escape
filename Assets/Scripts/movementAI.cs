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

        // Start is called before the first frame update
        void Start()
        {
            enemy = GetComponent<NavMeshAgent>();
            enemy.updateRotation = false;
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
            enemy.SetDestination(player.transform.position);
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
                if(door.locked == false  && !door.doOnce)
                {
                    door.PlayAnimation();
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

