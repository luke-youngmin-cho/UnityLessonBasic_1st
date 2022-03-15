using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private string selectedSceneName;
    public void SelectLevel(string sceneName)
    {
        selectedSceneName = sceneName;
    }
    public void PlaySelectedLevel()
    {
        GameManager.LoadSceneByName(selectedSceneName);
    }
}
