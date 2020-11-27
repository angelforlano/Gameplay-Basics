using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public CinemachineFreeLook camera; 
    
    PlayerController playerInGame;
    List<Enemy> enemiesInGame = new List<Enemy>();

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
        var playerGameObject = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        playerInGame = playerGameObject.GetComponent<PlayerController>();
        
        camera.Follow = playerInGame.transform;
        camera.LookAt = playerInGame.transform;

        HUDController.Instance.SetPlayer(playerInGame);
    }

    void Start()
    {
        SpawnPlayer();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemiesInGame.Add(enemy);

        Debug.Log("Enemies Count: " + enemiesInGame.Count);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInGame.Remove(enemy);
    }
}