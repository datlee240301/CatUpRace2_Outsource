using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetBooster : Booster
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Helmet"))
        // {
        //     Debug.Log("trigger helmet");
        //     ActivateBooster();
        //     SoundController.Instance.PlaySound(SoundType.EarnHelmet);
        //     EventDispatcher.PostEvent(EventID.ActiveBooster);
        // }
    }

    public override void ActivateBooster()
    {
        base.ActivateBooster();
        Debug.Log("Helmet booster activated");
        ActiveHelmet();
    }

    public void ActiveHelmet()
    {
        // var player = FindObjectOfType<Player>();
        // player.IsHelmet = true;
    }
}