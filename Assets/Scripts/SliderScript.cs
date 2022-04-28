using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class SliderScript : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] Slider volumeSlider;

        void Start()
        {
            if(!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
            }
            else
            {
                Load();
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
            Save();
        }

        public void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    }
}

