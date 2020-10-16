using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public PlayerController playerInGame;
    private List<Enemy> enemiesInGame = new List<Enemy>();

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