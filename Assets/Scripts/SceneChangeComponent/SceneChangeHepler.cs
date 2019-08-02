using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHepler : MonoBehaviour
{
    [Header("填写索要自动跳转的场景名称")]
    public string sceneName;

    private void Start()
    {
        SceneManager.LoadScene(sceneName);
    }
}
