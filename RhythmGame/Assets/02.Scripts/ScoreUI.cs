using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    static public ScoreUI instance;
    public int _score;
    public int score
    {
        set
        {
            scoringCoroutine = StartCoroutine(E_Scoring(_score, value, scoringTime));
            _score = value;

        }
        get { return _score; }
    }
    public Text scoreText;
    public float scoringTime = 0.1f;
    public Coroutine scoringCoroutine;

    private void Awake()
    {
        instance = this;
    }
    IEnumerator E_Scoring(int before, int after, float time)
    {
        int delta = (int)((after - before) / time);
        while (before < after)
        {
            before += (int)(delta * Time.deltaTime);

            if (before > after)
                before = after;

            scoreText.text = before.ToString();

            yield return null;
        }
    }
}
