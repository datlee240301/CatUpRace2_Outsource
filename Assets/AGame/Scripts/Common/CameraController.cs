using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;
    public float sceneWith = 6f;
    private float orthographicSize = 0f;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        orthographicSize = sceneWith * Screen.height / Screen.width;
        camera.orthographicSize = orthographicSize;
    }

    private void Update()
    {
        orthographicSize = sceneWith * Screen.height / Screen.width;
        camera.orthographicSize = orthographicSize;
    }
}
