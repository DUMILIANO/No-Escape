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
                
                key.SetActive(true);
                foreach(MeshCollider collider in lockCol)
                {
                    collider.enabled = false;
                }
                
            }

        }

        private void OnDestroy()
        {
            Rotatelock.Rotated -= CheckResults;
        }


    }
}
