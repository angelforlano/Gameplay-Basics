using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public float minimumLoadingTime = 0.45f;

    void Start()
    {
       StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(minimumLoadingTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelManager.Instance.levelToLoad);
        
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
