using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float minY = -3f;
    public float maxY = 3f;
    public float spawnX = 9f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, yPos, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
