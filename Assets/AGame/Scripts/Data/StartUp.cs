using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    private void Awake()
    {
        SessionPref.ReadData();
        //SessionPref.AddGoldRemaining(100000);
        //SessionPref.SetCurrentLevel(106);
        Application.targetFrameRate = 120;
    }

    private void Start()
    {

    }
}
