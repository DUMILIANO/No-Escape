using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class LockControl : MonoBehaviour
    {

        private int[] result, correctCombination;

        // Start is called before the first frame update
        void Start()
        {
            result = new int[] { 5, 5, 5, 5 };
            correctCombination = new int[] { 3, 2, 3, 3 };
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
                Debug.Log("Opened!");
            }

        }

        private void OnDestroy()
        {
            Rotatelock.Rotated -= CheckResults;
        }


    }
}
