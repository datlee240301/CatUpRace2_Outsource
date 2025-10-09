using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPos"))
        {
            Debug.Log("trigger spawn pos");
            EventDispatcher.PostEvent(EventID.SpawnNextBlock);
        }
    }

    private void Start()
    {
        EventDispatcher.RegisterListener(EventID.ActiveAllObjects, ActivateAllChildren);
    }

    private void OnEnable()
    {
        ActivateAllChildren(null);
    }


    public void DeactivateAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ActivateAllChildren(object data)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}