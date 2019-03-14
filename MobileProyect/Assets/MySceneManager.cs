using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

    string sceneToLoad;
    public string SceneToLoad {get{return sceneToLoad; }}
    static MySceneManager instance = new MySceneManager();
    public static MySceneManager Instance { get { return instance; } }

    private MySceneManager()
    {

    }

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);       
    }
    public void LoadSceneAsync()
    {
        StartCoroutine(Loading(sceneToLoad));
    }
    IEnumerator Loading (string sceneName)
    {        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        float progress = asyncLoad.progress;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
