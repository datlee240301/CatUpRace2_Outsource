using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, target.position.y, this.transform.position.z);
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         var movement = other.GetComponent<Movement>();
    //         movement.speed = 0;
    //         StartCoroutine(DelayToShowTryAgainPanel());
    //     }
    // }
    //
    // private IEnumerator DelayToShowTryAgainPanel()
    // {
    //     yield return new WaitForSeconds(.3f);
    //     UIController.Instance.ShowTryAgainPanel();
    // }
}