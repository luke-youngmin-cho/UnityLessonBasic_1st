using UnityEngine;
using UnityEngine.UI;
public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public Text FPSText;

    private void Update()
    {
        // ��� �����Ӵ� �ð���ȭ�� (������)
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        FPSText.text = Mathf.Ceil(fps).ToString();

    }
}
