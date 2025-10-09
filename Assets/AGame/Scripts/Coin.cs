using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Tốc độ xoay của coin
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Xoay coin quanh trục Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}