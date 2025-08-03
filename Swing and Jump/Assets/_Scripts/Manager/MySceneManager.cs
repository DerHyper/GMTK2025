using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    [SerializeField]
    private const string FLYING_SCENE = "FlyingScene";
    [SerializeField]
    private const string SWING_SCENE = "SwingScene";
    public Animator transition;
    public float transitionTime = 1;
    private string currentScene = SWING_SCENE;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        currentScene = SceneManager.GetActiveScene().name;
    }

    public void StartFlyingScene()
    {
        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("StartFadeIn");
    }

    public void LoadNextScene()
    {
        switch (currentScene)
        {
            case FLYING_SCENE:
                currentScene = SWING_SCENE;
                break;
            case SWING_SCENE:
                currentScene = FLYING_SCENE;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(currentScene);
    }

    internal void StartSwingingScene()
    {
        StartCoroutine(StartFadeIn());
    }
}