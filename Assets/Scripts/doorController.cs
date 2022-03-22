using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public Animator doorAnim;
    private bool open = false;
    public bool locked = false;
    public bool isRedDoor;
    public bool isWhiteDoor;
    public string animationName;
    public AudioClip doorOpeningSFX;
    AudioSource audio;

    private void Awake()
    {
        //doorAnim = gameObject.GetComponent<Animator>();
    }
    
    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void PlayAnimation()
    {
        if (!open && !locked)
        {
            doorAnim.Play("door Open", 0, 0.0f);
            open = true;
            audio.PlayOneShot(doorOpeningSFX);
        }
        else if (open)
        {
            doorAnim.Play("door Close", 0, 0.0f);
            open = false;
        }
    }
}
