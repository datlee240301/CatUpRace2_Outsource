using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public float intervalSpawnY = 1.0f;
    public float intervalSpawnX = 1.0f;
    public GameObject obstaclePrefab;
    public int countObstacle = 10;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        bool flipX = false;
        for (int i = 0; i < countObstacle; i++)
        {
            float x = intervalSpawnX;
            if (flipX)
            {
                x = -x;
            }
            flipX = !flipX;

            Vector3 position = new Vector3(x, intervalSpawnY * i, 0);
            Instantiate(obstaclePrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(.1f);
        }
    }
}
