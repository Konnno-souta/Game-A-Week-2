using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;


    void Start()
    {
        UpdateUI();
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }


    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }
}
