using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class bookChecker : MonoBehaviour
    {
        public doorController door;
        public Raycast raycast;
        public List<GameObject> container = new List<GameObject>();
        public int count;
        public int allCount;
        public bool puzzleComplete = false;
        bool doOnce = true;
        public string animationName;
        public Animator doorAnim;
        bool doOnceBooks = true;
        public TMP_Text bookShelfHint;
        public Animation nextScene;
        public TMP_Text dropPhoneText;


        void Update()
        {

            if(allCount == 6 && doOnceBooks == true && count != 6)
            {
                bookShelfHint.gameObject.SetActive(true);
                StartCoroutine(TextOffAfterTime());
                doOnceBooks = false;
            }
            foreach (GameObject book in container)
            {
                if(book.GetComponent<bookContainer>().rightBook == true && count == 6)
                {
                    door.locked = false;
                    puzzleComplete = true;
                    book.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                }

                if(book.transform.childCount > 1)
                {
                    Debug.Log("placed");
                }
            }


            if(puzzleComplete && doOnce)
            {
                nextScene.Play();
                StartCoroutine(NextScene());
            }
        }
        IEnumerator TextOffAfterTime()
        {
            yield return new WaitForSeconds(2f);
            bookShelfHint.gameObject.SetActive(false);
            dropPhoneText.gameObject.SetActive(false);
        }
        IEnumerator NextScene()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(4);
            yield return new WaitForSeconds(1f);
            dropPhoneText.gameObject.SetActive(true);
            StartCoroutine(TextOffAfterTime());

        }
    }
}