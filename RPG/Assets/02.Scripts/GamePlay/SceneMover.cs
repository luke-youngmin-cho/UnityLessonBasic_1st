using UnityEngine.SceneManagement;
public class SceneMover
{
    public static string currentSceneName;

    /// <summary>
    /// ���� �̵���Ű�� �Լ�
    /// </summary>
    /// <param name="newSceneName"> �̵��ϰ���� �� �̸� </param>
    /// <returns> �� �̵� ���� : true ���� : false </returns>
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
