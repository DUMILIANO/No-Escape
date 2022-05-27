using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Scripts
{
    public class Settings : MonoBehaviour
    {
        Resolution[] resolutions;
         public TMPro.TMP_Dropdown resolutionDRP;

        void Start()
        {
            resolutions = Screen.resolutions;

            resolutionDRP.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = i;
                    }
            }

            resolutionDRP.AddOptions(options);
            resolutionDRP.value = currentResolutionIndex;
            resolutionDRP.RefreshShownValue();
        }

        public void SetResolution (int currentResolutionIndex)
        {
            Resolution Resolution = resolutions[currentResolutionIndex];
            Screen.SetResolution(Resolution.width, Resolution.height, Screen.fullScreen);
        }

        public void SetFullScreen(bool isfullScreen)
        {
                Screen.fullScreen = isfullScreen;
        }
    }
}
