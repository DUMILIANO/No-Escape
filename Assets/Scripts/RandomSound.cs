using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
    {
    public AudioSource thunder;

    void Start()
    {
        CallAudio ();
    }

void CallAudio()
    {
        Invoke ("Thunder",  10);
    }

void Thunder()
    {
       // thunder.clip = thunder[Random.Range(0, thunder.Length)];
        thunder.Play();
        CallAudio ();
    }
}
