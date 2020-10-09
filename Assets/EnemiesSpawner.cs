using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnArea))]
public class EnemiesSpawner : MonoBehaviour
{
    [Range(1, 100)] public int count = 1;
    [Range(0.1f, 10f)] public float spawnSpeed = 0.5f;
    public bool onStart;
    public bool useRandom;
    public List<GameObject> enemies = new List<GameObject>();
    private SpawnArea spawnArea;

    void Start()
    {
        spawnArea = GetComponent<SpawnArea>();

        if (onStart)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = enemies[Random.Range(0, enemies.Count)];
            Instantiate(enemy, spawnArea.GetRandomArea(), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
