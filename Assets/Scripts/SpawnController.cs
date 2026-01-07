using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
 [Header("Массивы префабов по категориям")]
    public GameObject[] blockPrefabs;
    public GameObject[] enemyPrefabs;
    public GameObject[] friendPrefabs;

    [Header("Параметры спавна")]
    public float spawnXRange = 5f;
    public float spawnHeight = 10f;
    public float spawnInterval = 1f;
    public float spawnCountBlocks = 2; // Максимальное количество объектов блоков
    public float spawnCountEnemy = 6; // Максимальное количество объектов враждебных существ
    public float spawnCountFriendly = 8; // Максимальное количество объектов мирных существ

    private float lastSpawnTime;

    void Update()
    {
        // Создаем новый объект через равные промежутки времени
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            GameObject objectToSpawn = changeObject();
            SpawnObject(objectToSpawn);
            lastSpawnTime = Time.time;
        }
    }

    GameObject changeObject()
    {
        int blocksCount = 0;
        int enemiesCount = 0;
        int friendsCount = 0;
        List<GameObject> allObjectsToSpawn = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {   

            Transform child = transform.GetChild(i);
            switch (child.GetComponent<FallingObject>().type)
            {
                case "block":
                    blocksCount++;
                    break;
                case "enemy":
                    enemiesCount++;
                    break;
                case "friend":
                    friendsCount++;
                    break;
            }
        }

        if (blocksCount < spawnCountBlocks)
        {
            allObjectsToSpawn.AddRange(blockPrefabs);
        }
        if (enemiesCount < spawnCountEnemy)
        {
            allObjectsToSpawn.AddRange(enemyPrefabs);
        }
        if (friendsCount < spawnCountFriendly)
        {
            allObjectsToSpawn.AddRange(friendPrefabs);
        }
        
        if (allObjectsToSpawn != null && allObjectsToSpawn.Count > 0)
        {
            return allObjectsToSpawn[Random.Range(0, allObjectsToSpawn.Count)]; 
        }
        return null;
    }

    void SpawnObject(GameObject objectToSpawn)
    {
        if (objectToSpawn != null)
        {
            // генерируем позицию
            float spawnX = Random.Range(-spawnXRange, spawnXRange);
            Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0);

            // создаем объект
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, this.transform);  
        }
    }
}