using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyTypeA;  // Klasik d��man prefab�
    public GameObject enemyTypeB;  // Yeni zorlu d��man prefab�
    public float spawnInterval = 2f;
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
        Vector3 spawnPos = new Vector3(10f, Random.Range(-4f, 4f), 0f);
        GameObject prefabToSpawn;

        Debug.Log("Current Level: " + GameManager.Instance.currentLevel);

        if (GameManager.Instance != null && GameManager.Instance.currentLevel == 2)
        {
            if (Random.value < 0.3f)
            {
                prefabToSpawn = enemyTypeB;
                Debug.Log("Spawn: EnemyTypeB");
            }

            else
            {
                prefabToSpawn = enemyTypeA;
                Debug.Log("Spawn: EnemyTypeA");
            }
        }
        else
        {
            prefabToSpawn = enemyTypeA;
        }

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

}
