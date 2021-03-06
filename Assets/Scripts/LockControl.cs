using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class LockControl : MonoBehaviour
    {

        private int[] result, correctCombination;
        public GameObject key;
        public MeshCollider[] lockCol;
        public GameObject[] children;
        public GameObject lockObj;
        public bool solved = false;
        public Raycast raycast;
        public InventoryUI inventoryUI;

        // Start is called before the first frame update
        void Start()
        {
            result = new int[] { 10, 10, 10, 10};
            correctCombination = new int[] { 5, 1, 0, 8 };
            Rotatelock.Rotated += CheckResults;
        }

        // Update is called once per frame
        void CheckResults(string wheelName, int number)
        {
            switch (wheelName)
            {
                case "wheel1":
                    result[0] = number;
                    break;

                case "wheel2":
                    result[1] = number;
                    break;

                case "wheel3":
                    result[2] = number;
                    break;

                case "wheel4":
                    result[3] = number;
                    break;
            }
            if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3])
            {
                solved = true;
                key.SetActive(true);
                
                foreach(MeshCollider collider in lockCol)
                {
                    collider.enabled = false;
                }

                for (int a = 0; a < transform.childCount; a++)
                {
                    transform.GetChild(a).gameObject.SetActive(false);
                }
                raycast.crosshair.enabled = true;
                raycast.lockCam.SetActive(false);
                raycast.player.SetActive(true);
                inventoryUI.cursorIsLocked = true;
                raycast.leaveLockTxt.gameObject.SetActive(false);
                raycast.crosshair.color = Color.white;
                raycast.propLock.SetActive(true);
                StartCoroutine(LockSolved());
            }

        }

        IEnumerator LockSolved()
        {
            yield return new WaitForSeconds(0.5f);
            lockObj.GetComponent<Animation>().Play("lockanim");
        }

        private void OnDestroy()
        {
            Rotatelock.Rotated -= CheckResults;
        }


    }
}
