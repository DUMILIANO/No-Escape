﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public Animator doorAnim;
    private bool open = false;

    private void Awake()
    {
        //doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!open)
        {
            doorAnim.Play("Door Open", 0, 0.0f);
            open = true;
        }
        else if (open)
        {
            doorAnim.Play("Door Close", 0, 0.0f);
            open = false;
        }
    }
}