using UnityEngine;
using UnityEngine.UI;
public class FPSCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public Text FPSText;

    private void Update()
    {
        // 평균 프레임당 시간변화량 (대충계산)
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        FPSText.text = Mathf.Ceil(fps).ToString();

    }
}
