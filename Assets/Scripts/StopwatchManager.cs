using UnityEngine;
using UnityEngine.UI;

public class StopwatchManager : MonoBehaviour
{
    public Text timeText;
    public bool isRunning = false;

    private float elapsedTime = 0f;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timeText.text = FormatTime(elapsedTime);
        }
    }

    // 外部が時間を取得するためのプロパティ
    public float ElapsedTime => elapsedTime;

    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    string FormatTime(float time)
    {
        int min = (int)(time / 60);
        int sec = (int)(time % 60);
        return $"{min:00}:{sec:00}";
    }
}


