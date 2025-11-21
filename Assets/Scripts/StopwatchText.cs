using UnityEngine;
using UnityEngine.UI;

public class StopwatchText : MonoBehaviour
{
    public Text timeText; // 表示するText
    public bool isRunning = false;
    private float timer = 0f;

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
        }

        // 00:00.0 のフォーマットで表示
        int minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = timer % 60f;
        timeText.text = $"{minutes:00}:{seconds:00.0}";
    }

    // 外から操作する用
    public void StartTimer()
    {
        timer = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        isRunning = false;
    }

    public float GetTime()
    {
        return timer;
    }
}
