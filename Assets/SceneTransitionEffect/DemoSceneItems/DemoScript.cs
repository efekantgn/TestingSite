using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public SceneField TargetScene;

    public void LoadTargetScene()
    {
        SceneTransitionManager.Instance.LoadScene(TargetScene);
    }
    public void LoadTargetSceneAsync()
    {
        SceneTransitionManager.Instance.LoadSceneAsync(TargetScene);
    }

    private void OnEnable()
    {
        SceneTransitionManager.Instance.OnSceneLoadStarted.AddListener(LoadStarted);
        SceneTransitionManager.Instance.OnSceneLoadEnded.AddListener(LoadEnded);
    }

    private void OnDisable()
    {
        SceneTransitionManager.Instance.OnSceneLoadStarted.RemoveListener(LoadStarted);
        SceneTransitionManager.Instance.OnSceneLoadEnded.RemoveListener(LoadEnded);
    }

    private void LoadEnded()
    {
        Debug.Log("LoadEnded");
    }

    private void LoadStarted()
    {
        Debug.Log("LoadStarted");
    }
}
