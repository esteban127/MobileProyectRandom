using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoaderManager : MonoBehaviour {
    public Slider slider;

    private void Awake()
    {
        LoadSceneAsync();
    }

    public void LoadSceneAsync()
    {
        StartCoroutine(Loading(MySceneManager.Instance.SceneToLoad));
    }
    IEnumerator Loading(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        slider.value = asyncLoad.progress;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
