using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

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
                    
        }
        void OnCollisionEnter(Collision collision) 
        {
            Debug.Log("collision");
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("Dead");
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "collider")
            {
                Debug.Log("Heartbeat");
                audio.Play();
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if(other.tag == "collider")
            {
                Debug.Log("Heartbeat");
                audio.Pause();
            }
        }
    } 
}

