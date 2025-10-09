using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSlowBooster : Booster
{
    private Player player;
    public float slowSpeed = 30f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Speed"))
        {
            Debug.Log("trigger speed");
            ActivateBooster();
            //EventDispatcher.PostEvent(EventID.ActiveBooster);
        }
    }

    public override void ActivateBooster()
    {
        base.ActivateBooster();
        Debug.Log("speed booster activated");
        ActiveSlowSpeed();
    }

    public void ActiveSlowSpeed()
    {
        var movement = player.GetComponent<Movement>();
        //movement.SetSpeedPlayer(slowSpeed);
    }
}
