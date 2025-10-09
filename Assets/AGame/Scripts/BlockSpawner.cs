using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] block1Variants; // 2 biến thể của Block 1
    public GameObject[] block2Variants; // 2 biến thể của Block 2
    public GameObject[] block3Variants; // 2 biến thể của Block 3
    public GameObject[] sequentialBlockPrefabs; // Danh sách block từ 4 đến 15 (Prefab)

    public Transform target; // Transform target làm mốc kiểm tra vị trí Y
    public float hideDistanceY = -2f; // Khoảng cách Y dưới target để ẩn block
    public float spawnAboveTargetY = 5f; // Khoảng cách Y phía trên target để spawn block mới
    public float spawnDelay = 0.5f; // Độ trễ giữa các lần spawn block

    private List<GameObject> firstThreeBlocks = new List<GameObject>(); // 3 block đầu tiên
    private List<GameObject> sequentialBlocks = new List<GameObject>(); // Block từ 4 đến 15
    public int firstThreeIndex = 0;
    public int sequentialIndex = 0;
    public bool isSpawningFirstThree = true; // Kiểm soát trạng thái spawn
    public int spaceBetweenBlock = 1;
    public int currentBlockPosY = 0;
    public int firstBlockPosY = 5;

    void Start()
    {
        Debug.Log("Start method called");
        InitializeAllBlocks();
        StartCoroutine(SpawnFirstThreeBlocks());
        EventDispatcher.RegisterListener(EventID.SpawnNextBlock, OnSpawnNextBlock);
        EventDispatcher.RegisterListener(EventID.ResetGame, OnResetGame);
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy method called");
        EventDispatcher.RemoveListener(EventID.SpawnNextBlock, OnSpawnNextBlock);
        EventDispatcher.RemoveListener(EventID.ResetGame, OnResetGame);
    }

    private void LateUpdate()
    {

    }

    private void OnResetGame(object data)
    {
        TurnOffAllBlock();
        firstThreeIndex = 0;
        sequentialIndex = 0;
        isSpawningFirstThree = true;
        StartCoroutine(SpawnFirstThreeBlocks());
    }

    void InitializeAllBlocks()
    {
        Debug.Log("InitializeAllBlocks method called");
        // Ẩn toàn bộ block từ 1 đến 15 trước khi bắt đầu
        firstThreeBlocks.Clear();
        firstThreeBlocks.Add(Instantiate(block1Variants[Random.Range(0, block1Variants.Length)],this.transform));
        firstThreeBlocks.Add(Instantiate(block2Variants[Random.Range(0, block2Variants.Length)],this.transform));
        firstThreeBlocks.Add(Instantiate(block3Variants[Random.Range(0, block3Variants.Length)],this.transform));

        foreach (var block in firstThreeBlocks)
        {
            block.SetActive(false);
        }

        // Khởi tạo block từ 4 đến 15 và ẩn chúng
        sequentialBlocks.Clear();
        foreach (var prefab in sequentialBlockPrefabs)
        {
            GameObject block = Instantiate(prefab,this.transform);
            block.SetActive(false);
            sequentialBlocks.Add(block);
        }
    }

    IEnumerator SpawnFirstThreeBlocks()
    {
        //Debug.Log("SpawnFirstThreeBlocks coroutine started");
        // Spawn từng block 1, 2, 3 theo thứ tự
        while (firstThreeIndex < 1)
        {
            //Debug.Log("spawn first three blocks "+target.position.y);
            GameObject currentBlock = firstThreeBlocks[firstThreeIndex];
            float spawnY = 5;
            currentBlock.transform.position = new Vector3(0, spawnY, 0);
            currentBlock.SetActive(true);
            //Debug.Log($"Spawned block {firstThreeIndex + 1} at position {currentBlock.transform.position}");
            firstThreeIndex++;
            currentBlockPosY = firstBlockPosY + spaceBetweenBlock;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public GameObject currentBlock;
    void OnSpawnNextBlock(object data)
    {
        Debug.Log("OnSpawnNextBlock method called");
        if (isSpawningFirstThree)
        {
            Debug.Log("isSpawningFirstThree is true");
            if (firstThreeBlocks.Count == 0) return;
            firstThreeIndex++;
            currentBlock = firstThreeBlocks[firstThreeIndex - 1];

            currentBlock.SetActive(false);
            currentBlock.SetActive(true);
            currentBlock.transform.position =  new Vector3(0, currentBlockPosY , 0);
            currentBlockPosY += spaceBetweenBlock;

            if (firstThreeIndex >= firstThreeBlocks.Count)
            {
                isSpawningFirstThree = false; // Chuyển sang mode spawn block 4-15
                sequentialIndex = 0;
                //SpawnNextSequentialBlock();
                //StartCoroutine(SpawnSequentialBlocks());
            }
        }
        else
        {
            Debug.Log("isSpawningFirstThree is false");
            if (sequentialBlocks.Count == 0) return;
            SpawnNextSequentialBlock();
            // currentBlock = sequentialBlocks[sequentialIndex];
            // Debug.Log($"Hiding block {sequentialIndex + 4} at position {currentBlock.transform.position}");
            //
            // currentBlock.SetActive(true);
            // if (sequentialIndex >= sequentialBlocks.Count)
            // {
            //     sequentialIndex = 0; // Loop lại từ block thứ 4
            // }
            //SpawnNextSequentialBlock();
        }
    }

    void SpawnNextFirstThreeBlock()
    {
        Debug.Log("SpawnNextFirstThreeBlock method called");
        GameObject nextBlock = firstThreeBlocks[firstThreeIndex];
        float spawnY = target.position.y + spawnAboveTargetY;
        nextBlock.transform.position = new Vector3(0, spawnY, 0);
        nextBlock.SetActive(true);
        Debug.Log($"Spawned next block {firstThreeIndex + 1} at position {nextBlock.transform.position}");
    }

    IEnumerator SpawnSequentialBlocks()
    {
        Debug.Log("SpawnSequentialBlocks coroutine started");
        while (true) // Loop vô tận để duy trì tuần tự block từ 4 đến 15
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnNextSequentialBlock();
        }
    }

    void SpawnNextSequentialBlock()
    {
        // sequentialIndex++;
        // if (sequentialIndex >= sequentialBlocks.Count)
        // {
        //     sequentialIndex = 0; // Loop lại từ block thứ 4
        // }
        Debug.Log("SpawnNextSequentialBlock method called");
        GameObject nextBlock = sequentialBlocks[sequentialIndex];
        nextBlock.gameObject.SetActive(false);
        nextBlock.gameObject.SetActive(true);
        float spawnY = currentBlockPosY;
        nextBlock.transform.position = new Vector3(0, spawnY, 0);
        currentBlockPosY += spaceBetweenBlock;
        nextBlock.SetActive(true);
        sequentialIndex++;
        if (sequentialIndex >= sequentialBlocks.Count)
        {
            sequentialIndex = 0; // Loop lại từ block thứ 4
        }
        Debug.Log($"Spawned next sequential block {sequentialIndex + 4} at position {nextBlock.transform.position}");
    }

    void TurnOffAllBlock()
    {
        Debug.Log("TurnOffAllBlock method called");
        foreach (var block in firstThreeBlocks)
        {
            block.SetActive(false);
        }

        foreach (var block in sequentialBlocks)
        {
            block.SetActive(false);
        }
    }
}