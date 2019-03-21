using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    public string sceneToLoad;
    private void Start()
    {
        MySceneManager.Instance.LoadScene(sceneToLoad);
    }
}
