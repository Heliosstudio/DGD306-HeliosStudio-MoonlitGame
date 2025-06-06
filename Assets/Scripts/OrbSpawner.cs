using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public float spawnInterval = 5f;
    public float spawnRangeY = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnOrb();
            timer = 0f;
        }
    }

    void SpawnOrb()
    {
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPos = new Vector3(4f, randomY, 0f); 
        Instantiate(orbPrefab, spawnPos, Quaternion.identity);
    }
}
