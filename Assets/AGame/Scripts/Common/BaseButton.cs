using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BaseButton : MonoBehaviour
{
    public Button button;


    private void OnValidate()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    private void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SoundController.Instance.PlaySound(SoundType.ButtonClick);
        });
    }
}
