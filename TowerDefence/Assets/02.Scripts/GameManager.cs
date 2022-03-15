using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public static void LoadSceneByName(string sceneName)
    {
        Scene targetScene = SceneManager.GetSceneByName(sceneName);        
        if (targetScene.IsValid())
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"Can't find scene of {sceneName}");
            return;
        }
        
    }

}
public enum GameState
{    
    Idle,
    SelectLevel,
    StartLevel,
    WaitForLevelFinished,
    CompleteLevel,
    FailLevel,
    GameFinished
}