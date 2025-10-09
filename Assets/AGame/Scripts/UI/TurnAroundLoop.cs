using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundLoop : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float scaleSpeed = 1f;
    public float maxScale = 2f;
    public float minScale = 0.5f;

    private bool scalingUp = true;

    // Update is called once per frame
    void Update()
    {
        // Rotate around Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Scale up and down
        if (scalingUp)
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            if (transform.localScale.x >= maxScale)
            {
                scalingUp = false;
            }
        }
        else
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (transform.localScale.x <= minScale)
            {
                scalingUp = true;
            }
        }
    }
}
