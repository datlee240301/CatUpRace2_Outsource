using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] public List<GameObject> clouds;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float resetPositionY = 10.0f;
    [SerializeField] private float startPositionY = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeClouds();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveClouds();
    }

    private void InitializeClouds()
    {
        float positionY = startPositionY;
        foreach (var cloud in clouds)
        {
            cloud.transform.position = new Vector3(cloud.transform.position.x, positionY, cloud.transform.position.z);
            positionY -= 5.0f; // Adjust the spacing between clouds as needed
        }
    }

    private void MoveClouds()
    {
        foreach (var cloud in clouds)
        {
            cloud.transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (cloud.transform.position.y < target.position.y-resetPositionY)
            {
                cloud.transform.position = new Vector3(cloud.transform.position.x, target.position.y+resetPositionY, cloud.transform.position.z);
            }
        }
    }
}