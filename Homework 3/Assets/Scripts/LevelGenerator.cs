using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
    [Header("Prefabs")]
    public GameObject startChunkPrefab;
    public GameObject[] chunkPrefabs;
    public GameObject keyPrefab;
    public GameObject doorPrefab;
    public GameObject enemyPrefab;

    [Header("Level Option")]
    private int xOffset = 2;
    private int yOffset = 2;

    public int numberOfChunks = 3;
    public float chunkLength = 30f;
    public float minDistanceBetweenKeys = 12f;

    void Start() {
        GenerateLevel();
    }

    void GenerateLevel() {
        List<Transform> allSpawnPoints = new List<Transform>();
        float currentX = 0f;

        if (startChunkPrefab != null) {
            Instantiate(startChunkPrefab, new Vector3(currentX, 0, 0), Quaternion.identity);
            currentX += 10;
        }

        for (int i = 0; i < numberOfChunks; i++) {
            GameObject chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
            GameObject spawnedChunk = Instantiate(chunkToSpawn, new Vector3(currentX - xOffset, 0, 0), Quaternion.identity);

            foreach (Transform child in spawnedChunk.GetComponentsInChildren<Transform>()) {
                if (child.CompareTag("SpawnPoint")) {
                    allSpawnPoints.Add(child);
                }
            }
            currentX += chunkLength;
        }

        ShuffleList(allSpawnPoints);

        List<Vector3> spawnedKeyPositions = new List<Vector3>();
        int keysSpawned = 0;

        for (int i = allSpawnPoints.Count - 1; i >= 0; i--) {
            if (keysSpawned >= 3) break;

            Transform point = allSpawnPoints[i];

            bool isFarEnough = true;
            foreach (Vector3 keyPos in spawnedKeyPositions) {
                if (Vector2.Distance(point.position, keyPos) < minDistanceBetweenKeys) {
                    isFarEnough = false;
                    break;
                }
            }

            if (isFarEnough) {
                Instantiate(keyPrefab, point.position + new Vector3(0f,yOffset,0f), Quaternion.identity);
                spawnedKeyPositions.Add(point.position);
                allSpawnPoints.RemoveAt(i);
                keysSpawned++;
            }
        }

        if (allSpawnPoints.Count > 0) {
            Transform doorPoint = allSpawnPoints[0];
            Instantiate(doorPrefab, doorPoint.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            allSpawnPoints.RemoveAt(0);
        }

        List<Transform> safeEnemyPoints = new List<Transform>();
        foreach (Transform point in allSpawnPoints) {
            if (point.position.x > 15f) {
                safeEnemyPoints.Add(point);
            }
        }

        int enemiesToSpawn = Random.Range(2, 5);
        enemiesToSpawn = Mathf.Min(enemiesToSpawn, safeEnemyPoints.Count);

        for (int i = 0; i < enemiesToSpawn; i++) {
            Transform enemyPoint = safeEnemyPoints[i];
            Instantiate(enemyPrefab, enemyPoint.position, Quaternion.identity);
        }
    }

    void ShuffleList<T>(List<T> list) {
        for (int i = 0; i < list.Count; i++) {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}