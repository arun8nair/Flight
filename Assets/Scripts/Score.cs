using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private CourseGenerator courseGenerator;
    private int scoreVal;
    public int ScoreValue { get { return scoreVal; } }
    void Start()
    {
        scoreVal = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        courseGenerator = GameObject.Find("GameManager").GetComponent<CourseGenerator>();
        StartCoroutine(KeepScore());
    }
    IEnumerator KeepScore()
    {
        while(true)
        {
            float scoreMultiplier = 0.5f;
            scoreVal = (int)(courseGenerator.courseSpeed * Time.timeSinceLevelLoad * scoreMultiplier);
            scoreText.text = scoreVal.ToString();
            yield return new WaitForEndOfFrame();
        }
    }
    
    public void StopKeepingScore()
    {
        StopCoroutine(KeepScore());
    }
}
