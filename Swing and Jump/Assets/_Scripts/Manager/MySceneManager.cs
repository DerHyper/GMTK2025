using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    [SerializeField]
    private const string FLYING_SCENE = "FlyingScene";
    [SerializeField]
    private const string SWING_SCENE = "SwingScene";
    private const string END_SCENE = "EndScene";
    private const string START_SCENE = "StartScene";
    public Animator transition;
    public float transitionTime = 1;
    private string currentScene = SWING_SCENE;
    private bool gameEnded = false;


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

    private void Start() {
        if (currentScene == START_SCENE)
        {
            AudioManager.Instance.PlayIntroMusic();
        }
    }

    public void StartFlyingScene()
    {
        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("StartFadeIn"); // Also loads next Scene
    }

    public void StartFadeInInstand()
    {
        transition.SetTrigger("StartFadeIn"); // Also loads next Scene
    }

    public void LoadNextScene()
    {
        if (gameEnded)
        {
            currentScene = END_SCENE;
            gameEnded = false;
            SceneManager.LoadScene(currentScene);
            return;
        }

        switch (currentScene)
        {
            case FLYING_SCENE:
                currentScene = SWING_SCENE;
                break;
            case SWING_SCENE:
                currentScene = FLYING_SCENE;
                break;
            case END_SCENE:
                currentScene = START_SCENE;
                break;
            case START_SCENE:
                currentScene = SWING_SCENE;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(currentScene);
    }

    public void StartIntro()
    {
        
    }

    internal void StartSwingingScene()
    {
        StartCoroutine(StartFadeIn());
    }

    internal void StartEndingScene()
    {
        gameEnded = true;
        StartCoroutine(StartFadeIn()); 
    }
}