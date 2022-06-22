using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class PlayerPos : MonoBehaviour
    {
        private GameMaster gm;
        public Animation fade;
        void Awake()
        {
            fade.Play();    
            gm = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
            transform.position = gm.lastCheckPointPos;  
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}