using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelToLoad;
    public static LevelManager Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    IEnumerator LoadLevelRoutine()
    {
        HUDController.Instance.FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadLevel(string levelName)
    {
        levelToLoad = levelName;
        StartCoroutine(LoadLevelRoutine());
    }
}