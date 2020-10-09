using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public SpawnArea enemiesSpawn;
    public List<GameObject> enemies = new List<GameObject>();
    
    #region Singleton

    public static GameController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log(gameObject.name + " Single Instance!");
        } else {
            Destroy(gameObject);
        }
    }

    #endregion

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    void Start()
    {
        SpawnPlayer();
    }
}