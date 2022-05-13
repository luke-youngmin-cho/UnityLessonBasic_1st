using UnityEngine.SceneManagement;
public class SceneMover
{
    public static string currentSceneName;

    /// <summary>
    /// 씬을 이동시키는 함수
    /// </summary>
    /// <param name="newSceneName"> 이동하고싶은 씬 이름 </param>
    /// <returns> 씬 이동 성공 : true 실패 : false </returns>
    public static bool MoveScene(string newSceneName)
    {
        if (currentSceneName != newSceneName)
        {
            SceneManager.LoadScene(newSceneName);
            currentSceneName = newSceneName;
            return true;
        }
        return false;
    }
}
