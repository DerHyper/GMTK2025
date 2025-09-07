using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLoadingManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        MySceneManager.Instance.LoadNextScene(); // next is start
    }
}
